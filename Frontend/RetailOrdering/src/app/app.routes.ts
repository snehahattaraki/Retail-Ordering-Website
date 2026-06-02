import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth-guard';
import { adminGuard } from './core/guards/admin-guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./features/home/home').then((m) => m.Home),
  },
  {
    path: 'login',
    loadComponent: () => import('./features/auth/login/login').then((m) => m.Login),
  },
  {
    path: 'register',
    loadComponent: () => import('./features/auth/register/register').then((m) => m.Register),
  },
  {
    path: 'products',
    loadComponent: () => import('./features/products/product-list/product-list').then((m) => m.ProductList),
  },
  {
    path: 'products/:id',
    loadComponent: () => import('./features/products/product-details/product-details').then((m) => m.ProductDetails),
  },
  {
    path: 'cart',
    loadComponent: () => import('./features/cart/cart-page/cart-page').then((m) => m.CartPage),
  },
  {
    path: 'checkout',
    loadComponent: () => import('./features/cart/checkout/checkout').then((m) => m.Checkout),
    canActivate: [authGuard],
  },
  {
    path: 'orders',
    loadComponent: () => import('./features/orders/order-history/order-history').then((m) => m.OrderHistory),
    canActivate: [authGuard],
  },
  {
    path: 'orders/success',
    loadComponent: () => import('./features/orders/order-success/order-success').then((m) => m.OrderSuccess),
    canActivate: [authGuard],
  },
  {
    path: 'orders/:id',
    loadComponent: () => import('./features/orders/order-details/order-details').then((m) => m.OrderDetails),
    canActivate: [authGuard],
  },
  {
    path: 'profile',
    loadComponent: () => import('./features/profile/profile').then((m) => m.Profile),
    canActivate: [authGuard],
  },
  {
    path: 'admin',
    loadComponent: () => import('./features/admin/admin-shell').then((m) => m.AdminShell),
    canActivate: [adminGuard],
    children: [
      {
        path: '',
        loadComponent: () => import('./features/admin/dashboard/dashboard').then((m) => m.Dashboard),
      },
      {
        path: 'products',
        loadComponent: () => import('./features/admin/manage-products/manage-products').then((m) => m.ManageProducts),
      },
      {
        path: 'orders',
        loadComponent: () => import('./features/admin/manage-orders/manage-orders').then((m) => m.ManageOrders),
      },
      {
        path: 'categories',
        loadComponent: () => import('./features/admin/categories/categories').then((m) => m.AdminCategories),
      },
      {
        path: 'inventory',
        loadComponent: () => import('./features/admin/inventory/inventory').then((m) => m.Inventory),
      },
      {
        path: 'brands',
        loadComponent: () => import('./features/admin/brands/brands').then((m) => m.AdminBrands),
      },
      {
        path: 'users',
        loadComponent: () => import('./features/admin/manage-users/manage-users').then((m) => m.ManageUsers),
      },
    ],
  },
  {
    path: '**',
    redirectTo: '',
  },
];