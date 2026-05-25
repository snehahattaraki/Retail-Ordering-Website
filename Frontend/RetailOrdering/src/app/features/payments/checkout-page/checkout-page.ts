import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink, Router } from '@angular/router';
import { Observable, combineLatest, map } from 'rxjs';
import { CartItem } from '../../../models/cart-item.model';
import { Product } from '../../../models/product.model';
import { CartService } from '../../../services/cart.service';
import { ProductsService } from '../../../services/products.service';
import { PaymentsService } from '../../../services/payments.service';
import { ToastService } from '../../../services/toast.service';
import { PaymentPageComponent } from '../payment-page/payment-page';

@Component({
  standalone: true,
  selector: 'app-checkout-page',
  imports: [CommonModule, ReactiveFormsModule, RouterLink, PaymentPageComponent],
  templateUrl: './checkout-page.html',
  styleUrls: ['./checkout-page.css'],
})
export class CheckoutPageComponent {
  readonly checkoutForm!: any;
  readonly cartItems$!: Observable<(CartItem & { product?: Product; totalPrice: number })[]>;
  readonly subtotal$!: Observable<number>;

  appliedDiscount = 0;
  couponMessage = '';
  submitting = false;

  constructor(
    private fb: FormBuilder,
    private cartService: CartService,
    private productsService: ProductsService,
    private paymentsService: PaymentsService,
    private toastService: ToastService,
    private router: Router
  ) {
    this.checkoutForm = this.fb.group({
      coupon: [''],
      paymentMethod: ['card', Validators.required],
    });

    this.cartItems$ = combineLatest([
      this.cartService.items$,
      this.productsService.getProducts(),
    ]).pipe(
      map(([items, products]) =>
        items.map((item) => {
          const product = products.find((p) => p.productId === item.productId);
          return {
            ...item,
            product,
            totalPrice: product ? product.price * item.quantity : 0,
          };
        })
      )
    );

    this.subtotal$ = this.cartItems$.pipe(
      map((items) => items.reduce((sum, item) => sum + item.totalPrice, 0))
    );
  }

  applyCoupon(): void {
    const code = this.checkoutForm.value.coupon || '';
    this.paymentsService.applyCoupon(code).subscribe((result) => {
      this.appliedDiscount = result.discountPercent;
      this.couponMessage = result.message;
      if (result.discountPercent > 0) {
        this.toastService.show(result.message, 'success');
      } else {
        this.toastService.show(result.message, 'warning');
      }
    });
  }

  placeOrder(): void {
    this.submitting = true;
    combineLatest([this.cartItems$, this.subtotal$])
      .pipe(map(([items, subtotal]) => ({
        items,
        subtotal,
        discountPercent: this.appliedDiscount,
        paymentMethod: this.checkoutForm.value.paymentMethod,
        total: Math.max(0, subtotal * (1 - this.appliedDiscount / 100)),
      })))
      .subscribe((payload) => {
        this.paymentsService.placeOrder(payload).subscribe(() => {
          this.cartService.clearCart();
          this.toastService.show('Order placed successfully!', 'success');
          this.router.navigate(['/products']);
          this.submitting = false;
        });
      });
  }
}
