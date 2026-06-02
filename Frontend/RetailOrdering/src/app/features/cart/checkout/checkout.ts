import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CartService } from '../../../core/services/cart.service';
import { OrderService, OrderCreateRequest } from '../../../core/services/order.service';
import { Toast } from '../../../core/services/toast';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './checkout.html',
  styleUrls: ['./checkout.css'],
})
export class Checkout {
  private fb = inject(FormBuilder);
  private cartService = inject(CartService);
  private orderService = inject(OrderService);
  private toast = inject(Toast);
  private router = inject(Router);

  form: FormGroup;
  paymentMethods = ['Credit Card', 'Debit Card', 'UPI', 'Cash on Delivery'];
  items$ = this.cartService.items$;
  subtotal$ = this.cartService.subtotal$;

  constructor() {
    this.form = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^\+?[0-9]{7,15}$/)]],
      address: ['', Validators.required],
      city: ['', Validators.required],
      postalCode: ['', Validators.required],
      paymentMethod: [this.paymentMethods[0], Validators.required],
    });
  }

  get f() {
    return this.form.controls;
  }

  isProcessing = false;

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.toast.show('Please fix the errors before submitting.', 'warning');
      return;
    }

    this.items$.subscribe((items) => {
      if (!items.length) {
        this.toast.show('Your cart is empty.', 'warning');
        return;
      }

      this.isProcessing = true; // Trigger the spinner

      const payload: OrderCreateRequest = {
        shippingAddress: `${this.form.value.address}, ${this.form.value.city}, ${this.form.value.postalCode}`,
        paymentMethod: this.form.value.paymentMethod,
        phone: this.form.value.phone,
        items: items.map((item) => ({ productId: item.product.id, quantity: item.quantity })),
      };

      this.orderService.createOrder(payload).subscribe({
        next: () => {
          // Delay the redirect to simulate payment processing
          setTimeout(() => {
            this.isProcessing = false;
            this.cartService.clearCart();
            this.toast.show('Payment Successful! We have emailed your receipt.', 'success');
            this.router.navigate(['/orders']); // Redirect to history
          }, 2000); 
        },
        error: () => {
          this.isProcessing = false;
          this.toast.show('Unable to place order. Please try again.', 'danger');
        }
      });
    }).unsubscribe();
  }
}
