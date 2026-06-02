import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map, tap } from 'rxjs';
import { Api } from './api';
import { Product } from '../models/product.model';
import { API_ENDPOINTS } from '../constants/api.constants';
import { ApiResponse } from '../models/api-response.model';
import { Pagination } from '../models/pagination.model';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private readonly productsSubject =
    new BehaviorSubject<Product[]>([]);

  readonly products$ = this.productsSubject.asObservable();

  constructor(private api: Api) {}

  loadProducts(
    params?: Record<string, unknown>
  ): Observable<Pagination<Product>> {
    return this.api
      .get<ApiResponse<Pagination<Product>>>(API_ENDPOINTS.PRODUCTS, params)
      .pipe(
        map((response) => {
          const pagination = response.data;
          this.productsSubject.next(pagination.items);
          return pagination;
        })
      );
  }

  loadProduct(productId: number): Observable<Product> {
    return this.api
      .get<ApiResponse<Product>>(`${API_ENDPOINTS.PRODUCTS}/${productId}`)
      .pipe(map((response) => response.data));
  }
}