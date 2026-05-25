import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export type ToastType = 'success' | 'danger' | 'warning' | 'info';

export interface ToastMessage {
  id: number;
  title: string;
  message: string;
  type: ToastType;
}

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  private readonly toastSubject = new BehaviorSubject<ToastMessage[]>([]);
  readonly toasts$ = this.toastSubject.asObservable();

  show(message: string, type: ToastType = 'success', title = 'Notification', duration = 4000) {
    const toast: ToastMessage = {
      id: Date.now() + Math.round(Math.random() * 1000),
      type,
      message,
      title,
    };
    this.toastSubject.next([...this.toastSubject.value, toast]);
    window.setTimeout(() => this.dismiss(toast.id), duration);
  }

  dismiss(id: number) {
    this.toastSubject.next(this.toastSubject.value.filter((toast) => toast.id !== id));
  }
}
