import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export interface ToastMessage {
  id: number;
  text: string;
  type: 'success' | 'warning' | 'danger' | 'info';
}

@Injectable({ providedIn: 'root' })
export class ToastService {
  private messagesSubject = new BehaviorSubject<ToastMessage[]>([]);
  readonly messages$ = this.messagesSubject.asObservable();

  show(text: string, type: ToastMessage['type'] = 'success'): void {
    const message: ToastMessage = { id: Date.now(), text, type };
    this.messagesSubject.next([...this.messagesSubject.value, message]);
    setTimeout(() => this.remove(message.id), 4000);
  }

  remove(id: number): void {
    this.messagesSubject.next(this.messagesSubject.value.filter((message) => message.id !== id));
  }
}
