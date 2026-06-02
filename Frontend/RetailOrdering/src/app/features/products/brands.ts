import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class Brands {
  brands = signal<string[]>([]);

  constructor(private http: HttpClient) {}

  load() {
    this.http.get<string[]>(`${environment.apiUrl}/Products/brands`).subscribe((b) => this.brands.set(b));
  }
}
