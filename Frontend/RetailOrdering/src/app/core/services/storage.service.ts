import { inject, Injectable, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  private readonly platformId = inject(PLATFORM_ID);

  private get isBrowser(): boolean {
    return isPlatformBrowser(this.platformId);
  }

  getItem(key: string): string | null {
    if (!this.isBrowser) {
      return null;
    }

    try {
      return localStorage.getItem(key);
    } catch {
      return null;
    }
  }

  setItem(key: string, value: string): void {
    if (!this.isBrowser) {
      return;
    }

    try {
      localStorage.setItem(key, value);
    } catch {
      // noop
    }
  }

  removeItem(key: string): void {
    if (!this.isBrowser) {
      return;
    }

    try {
      localStorage.removeItem(key);
    } catch {
      // noop
    }
  }

  clear(): void {
    if (!this.isBrowser) {
      return;
    }

    try {
      localStorage.clear();
    } catch {
      // noop
    }
  }
}
