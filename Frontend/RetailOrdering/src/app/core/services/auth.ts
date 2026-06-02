import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { Api } from './api';
import { API_ENDPOINTS } from '../constants/api.constants';
import { StorageService } from './storage.service';

export interface AuthCredentials {
  email: string;
  password: string;
}

export interface RegisterData {
  name: string;
  email: string;
  password: string;
}

export interface AuthUser {
  email: string;
  name: string;
  roles: string[];
}
 
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly tokenKey = 'auth_token';

  private readonly userSubject = new BehaviorSubject<AuthUser | null>(null);

  readonly user$ = this.userSubject.asObservable();

  constructor(
    private api: Api,
    private router: Router,
    private storageService: StorageService
  ) {
    // Prevent SSR/Vite hydration issues
    setTimeout(() => {
      this.restoreSession();
    });
  }

  get authToken(): string | null {
    return this.storageService.getItem(this.tokenKey);
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
    return this.api
      .post<Record<string, unknown>>(
        `${API_ENDPOINTS.AUTH}/login`,
        credentials
      )
      .pipe(
        tap((response) => {
          const token = this.extractToken(response);

          if (!token) {
            throw new Error('Missing authentication token');
          }

          this.saveToken(token);
        }),
        map(() => this.userSubject.value)
      );

  }

  register(payload: RegisterData): Observable<AuthUser | null> {
    return this.api
      .post<Record<string, unknown>>(
        `${API_ENDPOINTS.AUTH}/register`,
        payload
      )
      .pipe(
        tap((response) => {
          const token = this.extractToken(response);

          if (token) {
            this.saveToken(token);
          }
        }),
        map(() => this.userSubject.value)
      );
  }

  logout(): void {
    this.storageService.removeItem(this.tokenKey);

    this.userSubject.next(null);

    this.router.navigate(['/login']);
  }

  hasRole(role: string): boolean {
    const user = this.userSubject.value;

    if (!user) {
      return false; 
    }

    return user.roles.some(
      (item) => item.toLowerCase() === role.toLowerCase()
    );
  }

  private restoreSession(): void {
    const token = this.authToken;

    if (!token) {
      return;
    }

    this.updateUserFromToken(token);
  }

  private saveToken(token: string): void {
    this.storageService.setItem(this.tokenKey, token);

    this.updateUserFromToken(token);
  }

  /**
   * Supports backend response:
   * {
   *   success: true,
   *   message: '',
   *   data: {
   *     token: ''
   *   }
   * }
   */
  private extractToken(data: Record<string, unknown>): string | null {
    if (!data) {
      return null;
    }

    // API response wrapper
    const responseData = data['data'] as
      | Record<string, unknown>
      | undefined;

    if (
      responseData?.['token'] &&
      typeof responseData['token'] === 'string'
    ) {
      return responseData['token'];
    }

    if (
      responseData?.['accessToken'] &&
      typeof responseData['accessToken'] === 'string'
    ) {
      return responseData['accessToken'];
    }

    // Fallback direct token
    if (typeof data['token'] === 'string') {
      return data['token'];
    }

    if (typeof data['accessToken'] === 'string') {
      return data['accessToken'];
    }

    return null;
  }

 private updateUserFromToken(token: string): void {
    const payload = this.decodeToken(token);

    const roles = this.extractRoles(payload);

    // Extract email, checking standard and Microsoft-specific claims
    const email = String(
      payload?.['email'] || 
      payload?.['sub'] || 
      payload?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'] || 
      ''
    );

    // Extract name, checking standard and Microsoft-specific claims
    const name = String(
      payload?.['name'] ||
      payload?.['unique_name'] ||
      payload?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] ||
      ''
    );

    this.userSubject.next({
      email,
      name,
      roles,
    });
    // console.log('JWT PAYLOAD:', payload);
    // console.log('USER ROLE:', roles);
    // console.log('CURRENT USER:', {
    //   email,
    //   name,
    //   roles
    // });
  }

  /**
   * SSR/Vite safe JWT decode
   */
  private decodeToken(token: string): Record<string, unknown> {
    try {
      const [, payload] = token.split('.');

      if (!payload) {
        return {};
      }

      let decodedPayload = '';

      // Browser
      if (typeof window !== 'undefined') {
        decodedPayload = window.atob(payload);
      }
      // Server/Vite/SSR
      else {
        decodedPayload = Buffer.from(
          payload,
          'base64'
        ).toString('utf-8');
      }

      return JSON.parse(decodedPayload);
    } catch {
      return {};
    }
  }

  private extractRoles(
    payload: Record<string, unknown>
  ): string[] {

    // roles: ["Admin"]
    const roles = payload?.['roles'] as
      | string[]
      | string
      | undefined;

    if (Array.isArray(roles)) {
      return roles;
    }

    // roles: "Admin"
    if (typeof roles === 'string') {
      return [roles];
    }

    // role: "Admin"
    const role = payload?.['role'];

    if (typeof role === 'string') {
      return [role];
    }

    // ASP.NET default role claim
    const claim =
      payload?.[
      'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ];

    if (typeof claim === 'string') {
      return [claim];
    }

    return [];
  }
}