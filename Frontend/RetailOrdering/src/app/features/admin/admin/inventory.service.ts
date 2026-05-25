import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { catchError, Observable, of, tap } from 'rxjs';

export interface InventoryItem {
  productId: number;
  name: string;
  stockQty: number;
}

@Injectable({
  providedIn: 'root',
})
export class InventoryService {
  private apiUrl = 'https://localhost:5001/api';
  inventory = signal<InventoryItem[]>([]);

  constructor(private http: HttpClient) {
    this.loadInventory().subscribe();
  }

  loadInventory(): Observable<InventoryItem[]> {
    return this.http.get<InventoryItem[]>(`${this.apiUrl}/inventory`).pipe(
      catchError(() => of([])),
      tap((items) => this.inventory.set(items))
    );
  }

  updateStock(productId: number, stockQty: number): Observable<InventoryItem> {
    return this.http.put<InventoryItem>(`${this.apiUrl}/inventory/${productId}`, { productId, stockQty }).pipe(
      catchError(() => of({ productId, name: '', stockQty } as InventoryItem)),
      tap(() => this.loadInventory().subscribe())
    );
  }
}
