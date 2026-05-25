import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { ApiService } from './api';
import { API_ENDPOINTS } from '../constants/api.constants';

export interface AuthCredentials {
  email: string;
  password: string;
}

export interface RegisterData {
  email: string;
  password: string;
}

export interface AuthUser {
  email: string;
  name?: string;
  roles: string[];
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly tokenKey = 'auth_token';
  private readonly userSubject = new BehaviorSubject<AuthUser | null>(null);
  readonly user$ = this.userSubject.asObservable();

  constructor(private api: ApiService, private router: Router) {
    this.restoreSession();
  }

  get authToken(): string | null {
    if (typeof localStorage === 'undefined') {
      return null;
    }
    return localStorage.getItem(this.tokenKey);
  }

  get isAuthenticated(): boolean {
    return !!this.authToken;
  }

  get isAdmin(): boolean {
    return this.hasRole('Admin');
  }

  get user(): AuthUser | null {
    return this.userSubject.value;
  }

  login(credentials: AuthCredentials): Observable<AuthUser | null> {
    return this.api.post<Record<string, unknown>>(`${API_ENDPOINTS.AUTH}/login`, credentials).pipe(
      tap((data) => {
        const token = this.extractToken(data);
        if (!token) {
          throw new Error('Missing authentication token');
        }
        this.saveToken(token);
      }),
      map(() => this.userSubject.value)
    );
  }

  register(payload: RegisterData): Observable<AuthUser | null> {
    return this.api.post<Record<string, unknown>>(`${API_ENDPOINTS.AUTH}/register`, payload).pipe(
      tap((data) => {
        const token = this.extractToken(data);
        if (token) {
          this.saveToken(token);
        }
      }),
      map(() => this.userSubject.value)
    );
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.userSubject.next(null);
    this.router.navigate(['/auth/login']);
  }

  hasRole(role: string): boolean {
    const user = this.userSubject.value;
    if (!user) {
      return false;
    }
    return user.roles.some((item) => item?.toLowerCase() === role.toLowerCase());
  }

  private restoreSession() {
    const token = this.authToken;
    if (!token) {
      return;
    }
    this.updateUserFromToken(token);
  }

  private saveToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
    this.updateUserFromToken(token);
  }

  private extractToken(data: Record<string, unknown>): string | null {
    if (!data) {
      return null;
    }
    if (typeof data['token'] === 'string') {
      return data['token'];
    }
    if (typeof data['accessToken'] === 'string') {
      return data['accessToken'];
    }
    if (typeof data === 'string') {
      return data;
    }
    return null;
  }

  private updateUserFromToken(token: string) {
    const payload = this.decodeToken(token);
    const roles = this.extractRoles(payload);
    const email = String(payload?.['email'] || payload?.['sub'] || '');
    const name = String(payload?.['name'] || payload?.['unique_name'] || '');
    this.userSubject.next({ email, name, roles });
  }

  private decodeToken(token: string): Record<string, unknown> {
    try {
      const [, payload] = token.split('.');
      if (!payload) {
        return {};
      }
      return JSON.parse(atob(payload));
    } catch {
      return {};
    }
  }

  private extractRoles(payload: Record<string, unknown>): string[] {
    const roles = payload?.['roles'] as string[] | string | undefined;
    if (Array.isArray(roles)) {
      return roles;
    }
    if (typeof roles === 'string') {
      return roles.split(',').map((role) => role.trim());
    }
    const claim = payload?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    if (typeof claim === 'string') {
      return [claim];
    }
    return [];
  }
}
