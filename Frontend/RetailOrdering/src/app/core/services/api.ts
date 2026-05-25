import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, throwError } from 'rxjs';
import { environment } from '../../../environments/environment';
import { API_ENDPOINTS } from '../constants/api.constants';

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
}

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private readonly baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  get<T>(path: string, params?: HttpParams) {
    return this.request<T>('GET', path, undefined, params);
  }

  post<T>(path: string, body: unknown) {
    return this.request<T>('POST', path, body);
  }

  put<T>(path: string, body: unknown) {
    return this.request<T>('PUT', path, body);
  }

  delete<T>(path: string) {
    return this.request<T>('DELETE', path);
  }

  private request<T>(method: string, path: string, body?: unknown, params?: HttpParams) {
    const url = this.buildUrl(path);
    return this.http
      .request<ApiResponse<T>>(method, url, { body, params })
      .pipe(
        map((response) => this.handleResponse(response)),
        catchError((error: HttpErrorResponse) => {
          const message = error.error?.message || error.message || 'Server request failed';
          return throwError(() => new Error(message));
        })
      );
  }

  private buildUrl(path: string) {
    const normalizedPath = path.startsWith('/') ? path : `/${path}`;
    return `${this.baseUrl}${normalizedPath}`;
  }

  private handleResponse<T>(response: ApiResponse<T>) {
    if (!response || response.success === false) {
      throw new Error(response?.message || 'API request failed');
    }
    return response.data;
  }
}
