import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CartItem } from '../models/cart-item.model';
import { Product } from '../models/product.model';
import { Api } from './api';
import { AuthService } from './auth';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private api = inject(Api);
  private authService = inject(AuthService);

  private readonly itemsSubject = new BehaviorSubject<CartItem[]>([]);
  readonly items$ = this.itemsSubject.asObservable();

  // Reactive subtotal calculation
  subtotal$ = this.items$.pipe(
    map(items => items.reduce((sum, item) => sum + item.product.price * item.quantity, 0))
  );

  // Reactive total items calculation
  totalItems$ = this.items$.pipe(
    map(items => items.reduce((sum, item) => sum + item.quantity, 0))
  );

  constructor() {
    // Automatically fetch the cart from the SQL Database when the app loads or user logs in!
    this.authService.user$.subscribe(user => {
      if (user) {
        this.api.get<any>('/Cart').subscribe({
          next: (res) => {
            this.itemsSubject.next(res.data || []);
          },
          error: () => this.itemsSubject.next([]) // Default to empty on error
        });
      } else {
        this.itemsSubject.next([]); // Clear cart from UI if logged out
      }
    });
  }

  // Push changes to SQL Database instantly
  private syncWithDatabase(items: CartItem[]): void {
    this.itemsSubject.next(items); // Update UI instantly

    if (this.authService.user) {
      const payload = items.map(i => ({ 
        productId: i.product.id, 
        quantity: i.quantity 
      }));
      
      // Save exact state to SQL
      this.api.post<any>('/Cart/sync', payload).subscribe();
    }
  }

  addToCart(product: Product, quantity = 1): void {
    const items = [...this.itemsSubject.value];
    const index = items.findIndex((item) => item.product.id === product.id);
    
    if (index === -1) {
      items.push({ product, quantity });
    } else {
      items[index] = {
        ...items[index],
        quantity: items[index].quantity + quantity,
      };
    }
    
    // Save to Database
    this.syncWithDatabase(items);
  }

  updateQuantity(productId: number, quantity: number): void {
    const items = this.itemsSubject.value.map((item) =>
      item.product.id === productId ? { ...item, quantity: Math.max(1, quantity) } : item
    );
    
    // Save to Database
    this.syncWithDatabase(items);
  }

  removeFromCart(productId: number): void {
    const items = this.itemsSubject.value.filter((item) => item.product.id !== productId);
    
    // Save to Database
    this.syncWithDatabase(items);
  }

  clearCart(): void {
    // Syncing an empty array will clear the items out of the database for this user
    this.syncWithDatabase([]);
  }
}