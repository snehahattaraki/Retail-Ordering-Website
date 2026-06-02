import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService, Product } from '../../../core/services/admin.service';
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
  get products() { return this.adminService.products; }
  
  // ADDED: Expose categories to the HTML
  get categories() { return this.adminService.categories; }
  get brands() { return this.adminService.brands; }

  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private adminService: AdminService,
    private loading: Loading,
    private toast: Toast
  ) {
    this.form = this.fb.group({
      id: [0],
      name: ['', Validators.required],
      category: ['', Validators.required], // Default to empty so they must choose
      brandId: ['', Validators.required], // ADDED
      price: [0, [Validators.required, Validators.min(0.01)]],
      stockQty: [0, [Validators.required, Validators.min(0)]],
      imageUrl: [''], 
    });
  }

  editProduct(product: Product): void {
    this.form.setValue({
      id: product.id || 0,
      name: product.name,
      category: product.category,
      brandId: product.id || '', // ADDED
      price: product.price,
      stockQty: product.stockQty,
      imageUrl: product.imageUrl || '', // ADDED
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

  deleteProduct(id: number): void {
    this.loading.track(this.adminService.deleteProduct(id)).subscribe(() => {
      this.toast.show('Product removed', 'info');
    });
  }

  // ADDED: Resolves brand identification mapping for display tracking
  getBrandName(id: number): string {
    //console.log('Looking up brand name for ID:', id);
    if (!id) return 'Generic';
    const match = this.brands().find(b => b.id === Number(id));
    return match ? match.name : 'Generic';
  }

  resetForm(): void {
    this.form.reset({ id: 0, name: '', category: '', price: 0, stockQty: 0 });
  }
}