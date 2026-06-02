import { Injectable, signal } from '@angular/core';
import { catchError, map, Observable, of, tap } from 'rxjs';
import { Api } from '../../core/services/api';
import { API_ENDPOINTS } from '../../core/constants/api.constants';
import { ApiResponse } from '../../core/models/api-response.model';
import { AuthService } from '../../core/services/auth';

export interface InventoryItem {
  id: number;
  name: string;
  stockQty: number;
  imageUrl?: string; // ADDED: Tracking image for dashboard display
}

@Injectable({
  providedIn: 'root',
})
export class InventoryService {
  inventory = signal<InventoryItem[]>([]);

  constructor(private api: Api, private authService: AuthService) {
    // FIX: Only load inventory data from backend if they have the rights to see it!
    this.authService.user$.subscribe(user => {
      if (user && (this.authService.hasRole('Admin') || this.authService.hasRole('Management'))) {
        this.loadInventory().subscribe();
      } else {
        this.inventory.set([]); // Clear data for Staff
      }
    });
  }

  loadInventory(): Observable<InventoryItem[]> {
    return this.api.get<ApiResponse<InventoryItem[]>>(API_ENDPOINTS.INVENTORY).pipe(
      map((res) => res.data ?? []),
      catchError(() => of([] as InventoryItem[])),
      tap((items) => this.inventory.set(items))
    );
  }

  updateStock(id: number, stockQty: number): Observable<InventoryItem> {
    return this.api.put<ApiResponse<InventoryItem>>(`${API_ENDPOINTS.INVENTORY}/${id}/stock`, { stockQty }).pipe(
      map((res) => res.data ?? ({ id, name: '', stockQty } as InventoryItem)),
      catchError(() => of({ id, name: '', stockQty } as InventoryItem)),
      tap(() => this.loadInventory().subscribe())
    );
  }
}