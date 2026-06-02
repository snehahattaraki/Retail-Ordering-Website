import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService, AdminBrand } from '../../../core/services/admin.service';
import { Loading } from '../../../core/services/loading';
import { Toast } from '../../../core/services/toast';

@Component({
  selector: 'app-admin-brands',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './brands.html'
})
export class AdminBrands {
  get brands() { return this.adminService.brands; }
  form!: FormGroup;

  constructor(private fb: FormBuilder, private adminService: AdminService, private loading: Loading, private toast: Toast) {
    this.form = this.fb.group({ id: [0], name: ['', Validators.required] });
  }

  saveBrand(): void {
    if (this.form.invalid) return;
    const brand = this.form.value as AdminBrand;
    this.loading.track(this.adminService.saveBrand(brand)).subscribe(() => {
      this.toast.show('Brand configuration locked', 'success');
      this.form.reset({ id: 0, name: '' });
    });
  }

  deleteBrand(id: number): void {
    if (confirm('Delete this brand parameter?')) {
      this.loading.track(this.adminService.deleteBrand(id)).subscribe(() => this.toast.show('Brand removed', 'info'));
    }
  }
}