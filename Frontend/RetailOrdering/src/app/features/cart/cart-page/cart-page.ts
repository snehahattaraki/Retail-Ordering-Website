import { Component, inject } from '@angular/core';
import { CommonModule, AsyncPipe, DecimalPipe } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { FormsModule } from '@angular/forms'; // ADDED for ngModel
import { CartService } from '../../../core/services/cart.service';
import { CartItem } from '../../../core/models/cart-item.model';
import { Api } from '../../../core/services/api';
import { Toast } from '../../../core/services/toast';

@Component({
  selector: 'app-cart-page',
  standalone: true,
  imports: [CommonModule, RouterLink, AsyncPipe, DecimalPipe, FormsModule],
  templateUrl: './cart-page.html',
  styleUrls: ['./cart-page.css'],
})
export class CartPage {
  private cartService = inject(CartService);
  private router = inject(Router);
  private api = inject(Api);
  private toast = inject(Toast);

  items$ = this.cartService.items$;
  subtotal$ = this.cartService.subtotal$;
  totalItems$ = this.cartService.totalItems$;

  couponCode = '';
  discountAmount = 0;

  updateQuantity(item: CartItem, quantity: number): void {
    this.cartService.updateQuantity(item.product.id, quantity);
  }

  removeItem(id: number): void {
    this.cartService.removeFromCart(id);
  }

  clearCart(): void {
    this.cartService.clearCart();
    this.discountAmount = 0;
  }

  applyCoupon(): void {
    if (!this.couponCode) {
      this.toast.show('Please enter a coupon code', 'warning');
      return;
    }

    // Grab the current subtotal synchronously from the observable
    let currentSubtotal = 0;
    this.subtotal$.subscribe(val => currentSubtotal = val).unsubscribe();

    if (currentSubtotal <= 0) {
      this.toast.show('Your cart is empty', 'warning');
      return;
    }
    
    // Send BOTH the code and the subtotal to the backend
    this.api.post<any>('/Coupons/apply', { 
      code: this.couponCode, 
      subtotal: currentSubtotal 
    }).subscribe({
      next: (res) => {
        const diff = res.data.total - res.data.discounted;
        this.discountAmount = diff;
        this.toast.show(`Coupon applied! You saved ₹${diff}`, 'success');
      },
      error: (err) => {
        // Dynamically show the actual error message from the C# backend!
        const errorMessage = err.error?.message || 'Invalid or expired coupon';
        this.toast.show(errorMessage, 'danger');
        this.discountAmount = 0;
      }
    });
  }

  checkout(): void {
    this.router.navigate(['/checkout']);
  }
}