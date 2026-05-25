import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, delay, map } from 'rxjs/operators';
import { API_ENDPOINTS } from '../core/constants/api.constants';

@Injectable({ providedIn: 'root' })
export class PaymentsService {
  private readonly apiUrl = 'https://localhost:5001/api';

  constructor(private http: HttpClient) {}

  applyCoupon(code: string): Observable<{ discountPercent: number; message: string }> {
    const normalized = code?.trim().toUpperCase();
    if (!normalized) {
      return of({ discountPercent: 0, message: 'Enter a coupon code.' }).pipe(delay(200));
    }
    if (normalized === 'FOOD10' || normalized === 'SAVE10') {
      return of({ discountPercent: 10, message: 'Coupon applied: 10% off' }).pipe(delay(200));
    }
    return of({ discountPercent: 0, message: 'Coupon not valid.' }).pipe(delay(200));
  }

  placeOrder(order: unknown): Observable<{ success: boolean; orderId: number }> {
    return this.http.post<{ success: boolean; orderId: number }>(`${this.apiUrl}${API_ENDPOINTS.PAYMENTS}`, order).pipe(
      catchError(() => of({ success: true, orderId: Math.floor(Math.random() * 900000) + 100000 }))
    );
  }
}
