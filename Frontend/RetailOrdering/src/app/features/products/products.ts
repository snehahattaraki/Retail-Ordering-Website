import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

export interface Product {
  productId: number;
  name: string;
  category: string;
  price: number;
  stockQty: number;
}

@Injectable({
  providedIn: 'root',
})
export class Products {
  products = signal<Product[]>([]);

  constructor(private http: HttpClient) {}

  load(): Observable<Product[]> {
    const obs = this.http.get<Product[]>(`${environment.apiUrl}/Products`);
    obs.subscribe((p) => this.products.set(p));
    return obs;
  }
}
