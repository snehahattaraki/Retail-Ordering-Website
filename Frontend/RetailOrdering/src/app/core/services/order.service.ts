import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map, tap } from 'rxjs';
import { Api } from './api';
import { Order } from '../models/order.model';
import { API_ENDPOINTS } from '../constants/api.constants';
import { ApiResponse } from '../models/api-response.model';

export interface OrderCreateRequest {
  shippingAddress: string;
  phone: string;
  paymentMethod: string;
  items: { productId: number; quantity: number }[];
}

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private readonly ordersSubject = new BehaviorSubject<Order[]>([]);
  private readonly orderSubject = new BehaviorSubject<Order | null>(null);

  readonly orders$ = this.ordersSubject.asObservable();
  readonly order$ = this.orderSubject.asObservable();

  constructor(private api: Api) {}

  loadOrders(): Observable<Order[]> {
    return this.api.get<ApiResponse<Order[]>>(API_ENDPOINTS.ORDERS).pipe(
      map((res) => res.data ?? []),
      tap((orders) => this.ordersSubject.next(orders))
    );
  }

  loadOrder(orderId: number): Observable<Order> {
    return this.api.get<ApiResponse<Order>>(`${API_ENDPOINTS.ORDERS}/${orderId}`).pipe(
      map((res) => res.data as Order),
      tap((order) => this.orderSubject.next(order))
    );
  }

  createOrder(payload: OrderCreateRequest): Observable<Order> {
    return this.api.post<ApiResponse<Order>>(`${API_ENDPOINTS.ORDERS}/place`, payload).pipe(
      map((res) => res.data as Order),
      tap((order) => {
        const items = [order, ...this.ordersSubject.value];
        this.ordersSubject.next(items);
      })
    );
  }
  reorder(orderId: number): Observable<any> {
  return this.api.post<ApiResponse<any>>(`${API_ENDPOINTS.ORDERS}/${orderId}/reorder`, {}).pipe(
    // Optionally refresh the orders list after a successful reorder
    tap(() => this.loadOrders().subscribe()) 
  );
}
}
