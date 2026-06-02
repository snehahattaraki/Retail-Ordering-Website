import { inject } from '@angular/core';
import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { Toast } from '../services/toast';
import { AuthService } from '../services/auth';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toast = inject(Toast);
  const router = inject(Router);
  const authService = inject(AuthService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      const message =
        error.error?.message || error.statusText || 'An unexpected API error occurred.';

      if (error.status === 401) {
        toast.show('Session expired. Please log in again.', 'warning');
        authService.logout();
        router.navigate(['/login']);
        return throwError(() => error);
      }

      if (error.status === 403) {
        toast.show('Access denied.', 'danger');
        router.navigate(['/']);
        return throwError(() => error);
      }

      if (error.status === 404) {
        toast.show('Requested resource not found.', 'warning');
        return throwError(() => error);
      }

      if (error.status >= 500) {
        toast.show('Server error. Please try again later.', 'danger');
        return throwError(() => error);
      }

      toast.show(message, 'danger');
      return throwError(() => error);
    })
  );
};
