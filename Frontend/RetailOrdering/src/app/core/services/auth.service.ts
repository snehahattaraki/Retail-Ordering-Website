import { PLATFORM_ID, inject, Injectable } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { ApiService } from './api.service';
import { API, ApiResponse, STORAGE } from '../constants/api.constants';

export interface AuthUser {
  name: string;
  email: string;
  roles: string[];
}

export interface AuthPayload {
  token: string;
  roles?: string[] | string;
  name?: string;
  email?: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly router = inject(Router);
  private readonly api = inject(ApiService);
  private readonly platformId = inject(PLATFORM_ID);

  private authSubject = new BehaviorSubject<boolean>(this.hasToken);
  private userSubject = new BehaviorSubject<AuthUser | null>(this.parseToken(this.token));

  readonly isAuthenticated$ = this.authSubject.asObservable();
  readonly user$ = this.userSubject.asObservable();

  private get isBrowser(): boolean {
    return isPlatformBrowser(this.platformId);
  }

  get token(): string | null {
    return this.isBrowser ? localStorage.getItem(STORAGE.token) : null;
  }

  get hasToken(): boolean {
    return !!this.token;
  }

  get isAdmin(): boolean {
    return this.userSubject.value?.roles.includes('Admin') ?? false;
  }

  login(credentials: { email: string; password: string }): Observable<ApiResponse<AuthPayload>> {
    return this.api.post<AuthPayload>(API.auth.login, credentials).pipe(
      tap((response) => {
        if (response.success && response.data?.token) {
          this.saveToken(response.data.token);
        }
      })
    );
  }

  register(payload: { name: string; email: string; password: string }): Observable<ApiResponse<AuthPayload>> {
    return this.api.post<AuthPayload>(API.auth.register, payload).pipe(
      tap((response) => {
        if (response.success && response.data?.token) {
          this.saveToken(response.data.token);
        }
      })
    );
  }

  logout(): void {
    if (this.isBrowser) {
      localStorage.removeItem(STORAGE.token);
    }
    this.authSubject.next(false);
    this.userSubject.next(null);
    this.router.navigate(['/auth/login'], { replaceUrl: true });
  }

  private saveToken(token: string): void {
    if (this.isBrowser) {
      localStorage.setItem(STORAGE.token, token);
    }
    this.authSubject.next(true);
    this.userSubject.next(this.parseToken(token));
  }

  private parseToken(token: string | null): AuthUser | null {
    if (!token) {
      return null;
    }

    try {
      const payload = JSON.parse(
        atob(token.split('.')[1].replace(/-/g, '+').replace(/_/g, '/'))
      );
      const rolesClaim = payload.role ?? payload.roles ?? [];
      const roles = Array.isArray(rolesClaim)
        ? rolesClaim
        : typeof rolesClaim === 'string'
        ? rolesClaim.split(',').map((value: string) => value.trim())
        : [];

      return {
        name: payload.name ?? payload.email ?? 'Customer',
        email: payload.email ?? '',
        roles,
      };
    } catch {
      return null;
    }
  }
}
