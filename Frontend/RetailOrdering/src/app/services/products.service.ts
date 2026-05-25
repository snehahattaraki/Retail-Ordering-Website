import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, map, of, tap } from 'rxjs';
import { Product } from '../models/product.model';
import { API_ENDPOINTS } from '../core/constants/api.constants';

@Injectable({ providedIn: 'root' })
export class ProductsService {
  private readonly apiUrl = 'https://localhost:5001/api';
  private productsSubject = new BehaviorSubject<Product[]>([]);
  readonly products$ = this.productsSubject.asObservable();

  constructor(private http: HttpClient) {}

  loadProducts(): void {
    this.http
      .get<Product[]>(`${this.apiUrl}${API_ENDPOINTS.PRODUCTS}`)
      .pipe(
        tap((items) => this.productsSubject.next(items)),
        catchError(() => {
          this.productsSubject.next([]);
          return of([] as Product[]);
        })
      )
      .subscribe();
  }

  getProducts(): Observable<Product[]> {
    if (!this.productsSubject.value.length) {
      this.loadProducts();
    }
    return this.products$;
  }

  getProduct(productId: number): Observable<Product | undefined> {
    return this.getProducts().pipe(
      map((products) => products.find((product) => product.productId === productId))
    );
  }
}
