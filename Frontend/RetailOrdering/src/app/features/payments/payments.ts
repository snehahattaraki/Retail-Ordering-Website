import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { Api } from '../../core/services/api';
import { API_ENDPOINTS } from '../../core/constants/api.constants';
import { ApiResponse } from '../../core/models/api-response.model';

@Injectable({
  providedIn: 'root',
})
export class Payments {
  constructor(private api: Api) {}

  process(orderId: number, payload: any): Observable<any> {
    return this.api.post<ApiResponse<any>>(`${API_ENDPOINTS.PAYMENTS}/${orderId}`, payload).pipe(
      map((res) => res.data)
    );
  }
}
