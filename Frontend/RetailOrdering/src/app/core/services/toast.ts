import { Injectable, signal } from '@angular/core';

export type ToastType = 'success' | 'danger' | 'warning' | 'info';

export interface ToastMessage {
  text: string;
  type: ToastType;
}

@Injectable({
  providedIn: 'root',
})
export class Toast {
  messages = signal<ToastMessage[]>([]);

  show(text: string, type: ToastType = 'success'): void {
    this.messages.update((list) => [...list, { text, type }]);
    setTimeout(() => this.dismiss(0), 4000);
  }

  dismiss(index: number): void {
    this.messages.update((list) => list.filter((_, i) => i !== index));
  }
}
