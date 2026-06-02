import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Toast } from '../../../core/services/toast';

@Component({
  selector: 'app-toast-notification',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './toast-notification.html',
  styleUrls: ['./toast-notification.css'],
})
export class ToastNotification {
  readonly toast = inject(Toast);
}
