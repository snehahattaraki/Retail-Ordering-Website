import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService, Product } from '../admin.service';
import { Loading } from '../../../core/services/loading';
import { Toast } from '../../../core/services/toast';

@Component({
  selector: 'app-manage-products',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './manage-products.html',
  styleUrls: ['./manage-products.css'],
})
export class ManageProducts {
  // In your component:
  get products() {
    return this.adminService.products; // This returns a WritableSignal<Product[]>
  }

  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private adminService: AdminService,
    private loading: Loading,
    private toast: Toast
  ) {
    this.form = this.fb.group({
      productId: [0],
      name: ['', Validators.required],
      category: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(0.01)]],
      stockQty: [0, [Validators.required, Validators.min(0)]],
    });
  }

  editProduct(product: Product): void {
    this.form.setValue({
      productId: product.productId,
      name: product.name,
      category: product.category,
      price: product.price,
      stockQty: product.stockQty,
    });
  }

  saveProduct(): void {
    if (this.form.invalid) {
      this.toast.show('Please fill in all fields', 'warning');
      return;
    }

    const product = this.form.value as Product;
    this.loading.track(this.adminService.saveProduct(product)).subscribe(() => {
      this.toast.show('Product saved successfully', 'success');
      this.resetForm();
    });
  }

  deleteProduct(productId: number): void {
    this.loading.track(this.adminService.deleteProduct(productId)).subscribe(() => {
      this.toast.show('Product removed', 'info');
    });
  }

  resetForm(): void {
    this.form.reset({ productId: 0, name: '', category: '', price: 0, stockQty: 0 });
  }
}