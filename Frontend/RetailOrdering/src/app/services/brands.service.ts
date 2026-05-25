import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, of } from 'rxjs';
import { Brand } from '../models/brand.model';
import { API_ENDPOINTS } from '../core/constants/api.constants';

@Injectable({ providedIn: 'root' })
export class BrandsService {
  private readonly apiUrl = 'https://localhost:5001/api';

  constructor(private http: HttpClient) {}

  getBrands(): Observable<Brand[]> {
    return this.http
      .get<Brand[]>(`${this.apiUrl}${API_ENDPOINTS.BRANDS}`)
      .pipe(catchError(() => of([] as Brand[])));
  }
}
