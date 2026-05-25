import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { catchError, Observable, of, tap } from 'rxjs';

export interface Product {
  productId: number;
  name: string;
  category: string;
  price: number;
  stockQty: number;
}

export interface User {
  userId: number;
  name: string;
  email: string;
  role: string;
}

export interface AdminOrder {
  orderId: number;
  userId: number;
  totalAmount: number;
  status: string;
}

export interface DashboardStats {
  totalProducts: number;
  totalUsers: number;
  totalOrders: number;
  totalRevenue: number;
}

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  private apiUrl = 'https://localhost:5001/api';
  products = signal<Product[]>([]);
  users = signal<User[]>([]);
  orders = signal<AdminOrder[]>([]);
  stats = signal<DashboardStats>({
    totalProducts: 0,
    totalUsers: 0,
    totalOrders: 0,
    totalRevenue: 0,
  });

  constructor(private http: HttpClient) {
    this.loadProducts().subscribe();
    this.loadUsers().subscribe();
    this.loadOrders().subscribe();
    this.loadStats().subscribe();
  }

  loadProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/inventory`).pipe(
      catchError(() => of([])),
      tap((products) => this.products.set(products))
    );
  }

  saveProduct(product: Product): Observable<Product> {
    const action = product.productId ? this.http.put<Product>(`${this.apiUrl}/inventory/${product.productId}`, product) : this.http.post<Product>(`${this.apiUrl}/inventory`, product);
    return action.pipe(
      catchError(() => of(product)),
      tap(() => this.loadProducts().subscribe())
    );
  }

  deleteProduct(productId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/inventory/${productId}`).pipe(
      catchError(() => of(null)),
      tap(() => this.loadProducts().subscribe())
    );
  }

  loadUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/admin/users`).pipe(
      catchError(() => of([])),
      tap((users) => this.users.set(users))
    );
  }

  saveUser(user: User): Observable<User> {
    const action = user.userId ? this.http.put<User>(`${this.apiUrl}/admin/users/${user.userId}`, user) : this.http.post<User>(`${this.apiUrl}/admin/users`, user);
    return action.pipe(
      catchError(() => of(user)),
      tap(() => this.loadUsers().subscribe())
    );
  }

  deleteUser(userId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/admin/users/${userId}`).pipe(
      catchError(() => of(null)),
      tap(() => this.loadUsers().subscribe())
    );
  }

  loadOrders(): Observable<AdminOrder[]> {
    return this.http.get<AdminOrder[]>(`${this.apiUrl}/orders`).pipe(
      catchError(() => of([])),
      tap((orders) => this.orders.set(orders))
    );
  }

  updateOrderStatus(orderId: number, status: string): Observable<AdminOrder> {
    return this.http.put<AdminOrder>(`${this.apiUrl}/orders/${orderId}`, { status }).pipe(
      catchError(() => of({ orderId, userId: 0, totalAmount: 0, status })),
      tap(() => this.loadOrders().subscribe())
    );
  }

  loadStats(): Observable<DashboardStats> {
    return this.http.get<DashboardStats>(`${this.apiUrl}/admin/stats`).pipe(
      catchError(() => of({ totalProducts: this.products().length, totalUsers: this.users().length, totalOrders: this.orders().length, totalRevenue: this.orders().reduce((sum, order) => sum + order.totalAmount, 0) })),
      tap((stats) => this.stats.set(stats))
    );
  }
}
