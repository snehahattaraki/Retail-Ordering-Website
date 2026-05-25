import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { AuthCredentials, AuthService } from '../../../core/services/auth';
import { LoadingService } from '../../../core/services/loading';
import { ToastService } from '../../../core/services/toast';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
})
export class Login {
  private readonly auth = inject(AuthService);
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly loadingService = inject(LoadingService);
  private readonly toastService = inject(ToastService);

  readonly form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
  });

  get email() {
    return this.form.get('email');
  }

  get password() {
    return this.form.get('password');
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loadingService.show();
    this.auth.login(this.form.value as AuthCredentials).pipe(finalize(() => this.loadingService.hide())).subscribe({
      next: () => {
        this.toastService.show('Login successful', 'success', 'Welcome back');
        this.router.navigate(['/home']);
      },
      error: (error) => {
        this.toastService.show(error?.message || 'Unable to login', 'danger', 'Login failed');
      },
    });
  }
}
