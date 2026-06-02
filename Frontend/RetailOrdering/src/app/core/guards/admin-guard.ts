import { inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';
import { AuthService } from '../services/auth';

export const adminGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  // 1. If they aren't logged in at all, send them to login
  if (!authService.isAuthenticated) {
    return router.parseUrl('/login');
  }

  // 2. Allow any employee role to access the admin shell
  if (
    authService.hasRole('Admin') || 
    authService.hasRole('Management') || 
    authService.hasRole('Staff')
  ) {
    return true;
  }

  // 3. If they are just a standard "Customer" (or have no role), kick them back to the homepage
  return router.parseUrl('/');
};