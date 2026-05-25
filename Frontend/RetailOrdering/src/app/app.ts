import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { FooterComponent } from './shared/components/footer/footer';
import { ToastService } from './core/services/toast';
import { LoadingService } from './core/services/loading';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NavbarComponent, FooterComponent],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  readonly toastService = inject(ToastService);
  readonly loadingService = inject(LoadingService);

  readonly toasts = this.toastService.toasts$;
  readonly loading$ = this.loadingService.loading$;
  readonly title = signal('Food Ordering');
}
