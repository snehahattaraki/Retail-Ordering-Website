import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { catchError, Observable, of, tap } from 'rxjs';

export interface Order {
  orderId: number;
  userId: number;
  totalAmount: number;
  status: string;
}

export interface OrderItem {
  orderItemId: number;
  orderId: number;
  productId: number;
  quantity: number;
  price: number;
}

@Injectable({
  providedIn: 'root',
})
export class OrdersService {
  private apiUrl = 'https://localhost:5001/api';
  orders = signal<Order[]>([]);
  order = signal<Order | null>(null);
  items = signal<OrderItem[]>([]);

  constructor(private http: HttpClient) {
    this.loadOrders().subscribe();
  }

  loadOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/orders`).pipe(
      catchError(() => of([])),
      tap((orders) => this.orders.set(orders))
    );
  }

  loadOrder(orderId: number): Observable<Order | null> {
    return this.http.get<Order>(`${this.apiUrl}/orders/${orderId}`).pipe(
      catchError(() => of(null)),
      tap((order) => this.order.set(order))
    );
  }

  loadOrderItems(orderId: number): Observable<OrderItem[]> {
    return this.http.get<OrderItem[]>(`${this.apiUrl}/orders/${orderId}/items`).pipe(
      catchError(() => of([])),
      tap((items) => this.items.set(items))
    );
  }

  reorder(orderId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/orders/${orderId}/reorder`, {}).pipe(catchError(() => of({ success: false })));
  }
}
