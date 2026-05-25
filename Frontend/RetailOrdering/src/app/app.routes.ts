import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  {
    path: 'products',
    loadComponent: () => import('./features/products/product-list/product-list').then((m) => m.ProductListComponent),
  },
  {
    path: 'products/:id',
    loadComponent: () => import('./features/products/product-details/product-details').then((m) => m.ProductDetailsComponent),
  },
  {
    path: 'cart',
    loadComponent: () => import('./features/cart/cart-page/cart-page').then((m) => m.CartPageComponent),
  },
  {
    path: 'checkout',
    loadComponent: () => import('./features/payments/checkout-page/checkout-page').then((m) => m.CheckoutPageComponent),
  },
  { path: '**', redirectTo: 'products' },
];
