import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Cart } from '../../cart/cart';
import { Payments } from '../payments';

@Component({
  selector: 'app-payment-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './payment-page.html',
  styleUrls: ['./payment-page.css'],
})
export class PaymentPage {
  form: FormGroup;
  isProcessing = false;

  constructor(private fb: FormBuilder, private router: Router, public cart: Cart, private payments: Payments) {
    this.form = this.fb.group({
      cardNumber: ['', [Validators.required, Validators.minLength(12), Validators.maxLength(19)]],
      cardName: ['', Validators.required],
      expiry: ['', Validators.required],
      cvv: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(4)]],
    });
  }

  get items() {
    return this.cart.items();
  }

  get totalItems(): number {
    return this.items.reduce((sum, item) => sum + item.quantity, 0);
  }

  submit() {
    if (this.items.length === 0) {
      this.router.navigate(['/cart']);
      return;
    }
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isProcessing = true;
    this.payments.process(0, this.form.value).subscribe({
      next: () => {
        this.cart.clear();
        this.router.navigate(['/orders', 'success']);
      },
      error: () => {
        this.isProcessing = false;
      },
    });
  }
}
