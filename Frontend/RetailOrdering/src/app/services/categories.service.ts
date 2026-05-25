import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, of } from 'rxjs';
import { Category } from '../models/category.model';
import { API_ENDPOINTS } from '../core/constants/api.constants';

@Injectable({ providedIn: 'root' })
export class CategoriesService {
  private readonly apiUrl = 'https://localhost:5001/api';

  constructor(private http: HttpClient) {}

  getCategories(): Observable<Category[]> {
    return this.http
      .get<Category[]>(`${this.apiUrl}${API_ENDPOINTS.CATEGORIES}`)
      .pipe(catchError(() => of([] as Category[])));
  }
}
