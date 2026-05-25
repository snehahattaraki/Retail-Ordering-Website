import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CartItem } from '../models/cart-item.model';
import { Product } from '../models/product.model';

const STORAGE_KEY = 'retail-ordering-cart';

@Injectable({ providedIn: 'root' })
export class CartService {
  private itemsSubject = new BehaviorSubject<CartItem[]>(this.loadCart());
  readonly items$ = this.itemsSubject.asObservable();

  get items(): CartItem[] {
    return this.itemsSubject.value;
  }

  addToCart(product: Product, quantity = 1): void {
    const items = [...this.items];
    const existing = items.find((item) => item.productId === product.productId);
    if (existing) {
      existing.quantity = Math.min(existing.quantity + quantity, product.stockQty || 99);
    } else {
      items.push({ cartItemId: Date.now(), productId: product.productId, quantity });
    }
    this.updateCart(items);
  }

  updateQuantity(productId: number, quantity: number): void {
    const items = this.items.map((item) =>
      item.productId === productId ? { ...item, quantity: Math.max(1, quantity) } : item
    );
    this.updateCart(items);
  }

  removeItem(productId: number): void {
    this.updateCart(this.items.filter((item) => item.productId !== productId));
  }

  clearCart(): void {
    this.updateCart([]);
  }

  private updateCart(items: CartItem[]): void {
    this.itemsSubject.next(items);
    this.saveCart(items);
  }

  private loadCart(): CartItem[] {
    if (typeof window === 'undefined') {
      return [];
    }
    try {
      const json = localStorage.getItem(STORAGE_KEY);
      return json ? (JSON.parse(json) as CartItem[]) : [];
    } catch {
      return [];
    }
  }

  private saveCart(items: CartItem[]): void {
    if (typeof window === 'undefined') {
      return;
    }
    localStorage.setItem(STORAGE_KEY, JSON.stringify(items));
  }
}
