import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService, User } from '../../../core/services/admin.service';
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
  get roles(){
    
    return this.adminService.roles; // ADDED: Expose roles to the HTML
  }

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
      email: ['', [Validators.required, Validators.email]],
      role: ['', Validators.required],
    });
  }

  editUser(user: User): void {
    this.form.setValue({
      id: user.id || 0,
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

  // Update delete and reset methods
  deleteUser(id: number): void {
    this.loading.track(this.adminService.deleteUser(id)).subscribe(() => {
      this.toast.show('User removed', 'info');
    });
  }

  resetForm(): void {
    this.form.reset({ id: 0, name: '', email: '', role: '' });
  }
}
