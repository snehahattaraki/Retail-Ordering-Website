import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { AppNavbar } from './shared/components/navbar/navbar';
import { AppFooter } from './shared/components/footer/footer';
import { LoadingSpinner } from './shared/components/loading-spinner/loading-spinner';
import { ToastNotification } from './shared/components/toast-notification/toast-notification';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, AppNavbar, AppFooter, LoadingSpinner, ToastNotification],
  templateUrl: './app.html',
  styleUrls: ['./app.css'],
})
export class App {}
