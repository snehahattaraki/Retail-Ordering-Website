import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { Home } from './features/home/home';
import { Dashboard } from './features/admin/dashboard/dashboard';
import { authGuard } from './core/guards/auth-guard';
import { adminGuard } from './core/guards/admin-guard';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'auth/login', component: Login },
  { path: 'auth/register', component: Register },
  { path: 'home', component: Home, canActivate: [authGuard] },
  {
    path: 'admin/dashboard',
    component: Dashboard,
    canActivate: [authGuard, adminGuard],
  },
  { path: '**', redirectTo: 'home' },
];
