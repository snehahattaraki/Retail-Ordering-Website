import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService, AdminCategory } from '../../../core/services/admin.service';
import { Loading } from '../../../core/services/loading';
import { Toast } from '../../../core/services/toast';
import { AuthService } from '../../../core/services/auth'; // ADDED: Import Auth

@Component({
  selector: 'app-admin-categories',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './categories.html'
})
export class AdminCategories {
  get categories() {
    return this.adminService.categories;
  }

  get isAdmin(): boolean {
    return this.authService.hasRole('Admin');
  }

  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private adminService: AdminService,
    private loading: Loading,
    private toast: Toast,
    private authService: AuthService // ADDED: Inject Auth
  ) {
    this.form = this.fb.group({
      id: [0],
      name: ['', Validators.required],
    });
  }

  editCategory(category: AdminCategory): void {
    this.form.setValue({
      id: category.id || 0,
      name: category.name,
    });
  }

  saveCategory(): void {
    if (this.form.invalid) {
      this.toast.show('Please provide a category name', 'warning');
      return;
    }

    const category = this.form.value as AdminCategory;
    this.loading.track(this.adminService.saveCategory(category)).subscribe(() => {
      this.toast.show('Category saved successfully', 'success');
      this.resetForm();
    });
  }

  deleteCategory(id: number): void {
    if (confirm('Are you sure? Products linked to this category might be affected.')) {
      this.loading.track(this.adminService.deleteCategory(id)).subscribe({
        next: () => this.toast.show('Category removed', 'info'),
        error: () => this.toast.show('Cannot delete category in use', 'danger')
      });
    }
  }

  resetForm(): void {
    this.form.reset({ id: 0, name: '' });
  }
}