import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { AsyncPipe, CommonModule, NgForOf } from '@angular/common';
import { ToastService } from './services/toast.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, NgForOf, AsyncPipe],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  constructor(public toastService: ToastService) {}
}
