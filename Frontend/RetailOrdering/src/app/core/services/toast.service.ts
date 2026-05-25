import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export type ToastLevel = 'success' | 'warning' | 'danger' | 'info';

export interface ToastMessage {
  id: string;
  title: string;
  message: string;
  level: ToastLevel;
}

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  private toastsSubject = new BehaviorSubject<ToastMessage[]>([]);
  readonly toasts$ = this.toastsSubject.asObservable();

  success(message: string, title = 'Success'): void {
    this.show(message, title, 'success');
  }

  warning(message: string, title = 'Warning'): void {
    this.show(message, title, 'warning');
  }

  error(message: string, title = 'Error'): void {
    this.show(message, title, 'danger');
  }

  info(message: string, title = 'Info'): void {
    this.show(message, title, 'info');
  }

  close(id: string): void {
    this.toastsSubject.next(this.toastsSubject.value.filter((toast) => toast.id !== id));
  }

  private show(message: string, title: string, level: ToastLevel): void {
    const toast: ToastMessage = {
      id: `${Date.now()}-${Math.random().toString(36).slice(2)}`,
      title,
      message,
      level,
    };

    this.toastsSubject.next([...this.toastsSubject.value, toast]);

    setTimeout(() => this.close(toast.id), 4500);
  }
}
