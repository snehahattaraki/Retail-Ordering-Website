import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService, User } from '../admin.service';
import { Loading } from '../../../core/services/loading';
import { Toast } from '../../../core/services/toast';

@Component({
  selector: 'app-manage-users',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './manage-users.html',
  styleUrls: ['./manage-users.css'],
})
export class ManageUsers {
  get users() {
    return this.adminService.users;
  }

  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private adminService: AdminService,
    private loading: Loading,
    private toast: Toast
  ) {
    this.form = this.fb.group({
      userId: [0],
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      role: ['Customer', Validators.required],
    });
  }

  editUser(user: User): void {
    this.form.setValue({
      userId: user.userId,
      name: user.name,
      email: user.email,
      role: user.role,
    });
  }

  saveUser(): void {
    if (this.form.invalid) {
      this.toast.show('Please complete the user form', 'warning');
      return;
    }

    const user = this.form.value as User;
    this.loading.track(this.adminService.saveUser(user)).subscribe(() => {
      this.toast.show('User saved successfully', 'success');
      this.resetForm();
    });
  }

  deleteUser(userId: number): void {
    this.loading.track(this.adminService.deleteUser(userId)).subscribe(() => {
      this.toast.show('User removed', 'info');
    });
  }

  resetForm(): void {
    this.form.reset({ userId: 0, name: '', email: '', role: 'Customer' });
  }
}
