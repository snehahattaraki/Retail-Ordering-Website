import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map, tap } from 'rxjs';
import { Api } from './api';
import { Category } from '../models/category.model';
import { API_ENDPOINTS } from '../constants/api.constants';
import { ApiResponse } from '../models/api-response.model';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private readonly categoriesSubject = new BehaviorSubject<Category[]>([]);
  readonly categories$ = this.categoriesSubject.asObservable();

  constructor(private api: Api) {}

  loadCategories(): Observable<Category[]> {
    return this.api.get<ApiResponse<Category[]>>(API_ENDPOINTS.CATEGORIES).pipe(
      map((response) => {
        const categories = response.data;
        this.categoriesSubject.next(categories);
        return categories;
      })
    );
  }

  saveCategory(category: Category): Observable<Category> {
    return this.api.post<ApiResponse<Category>>(API_ENDPOINTS.CATEGORIES, category).pipe(
      map((response) => response.data),
      tap(() => this.loadCategories().subscribe())
    );
  }
}
