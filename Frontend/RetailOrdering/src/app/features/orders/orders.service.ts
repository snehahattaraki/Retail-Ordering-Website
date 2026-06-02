import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { catchError, Observable, of, tap } from 'rxjs';
import { environment } from '../../../environments/environment';

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
  private apiUrl = environment.apiUrl;
  orders = signal<Order[]>([]);
  order = signal<Order | null>(null);
  items = signal<OrderItem[]>([]);

  constructor(private http: HttpClient) {
    this.loadOrders().subscribe();
  }

  loadOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/Orders`).pipe(
      catchError(() => of([])),
      tap((orders) => this.orders.set(orders))
    );
  }

  loadOrder(orderId: number): Observable<Order | null> {
    return this.http.get<Order>(`${this.apiUrl}/Orders/${orderId}`).pipe(
      catchError(() => of(null)),
      tap((order) => this.order.set(order))
    );
  }

  loadOrderItems(orderId: number): Observable<OrderItem[]> {
    return this.http.get<OrderItem[]>(`${this.apiUrl}/Orders/${orderId}/items`).pipe(
      catchError(() => of([])),
      tap((items) => this.items.set(items))
    );
  }

  reorder(orderId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/Orders/${orderId}/reorder`, {}).pipe(catchError(() => of({ success: false })));
  }
}
