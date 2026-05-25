import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { AuthService } from '../../../core/services/auth';
import { LoadingService } from '../../../core/services/loading';
import { ToastService } from '../../../core/services/toast';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrls: ['./register.css'],
})
export class Register {
  private readonly auth = inject(AuthService);
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly loadingService = inject(LoadingService);
  private readonly toastService = inject(ToastService);

  readonly form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', [Validators.required]],
  });

  get email() {
    return this.form.get('email');
  }

  get password() {
    return this.form.get('password');
  }

  get confirmPassword() {
    return this.form.get('confirmPassword');
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    if (this.password?.value !== this.confirmPassword?.value) {
      this.toastService.show('Passwords must match', 'warning', 'Validation error');
      return;
    }

    this.loadingService.show();
    this.auth.register({
      email: this.email?.value ?? '',
      password: this.password?.value ?? '',
    })
      .pipe(finalize(() => this.loadingService.hide()))
      .subscribe({
        next: () => {
          this.toastService.show('Registration complete', 'success', 'Account created');
          this.router.navigate(['/home']);
        },
        error: (error) => {
          this.toastService.show(error?.message || 'Unable to register', 'danger', 'Registration failed');
        },
      });
  }
}
