import { Component, computed, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterModule } from '@angular/router';
import { AuthService } from '../../../core/services/auth';
import { CartService } from '../../../core/services/cart.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule, RouterLink],
  templateUrl: './navbar.html',
  styleUrls: ['./navbar.css'],
})
export class AppNavbar {
  private authService = inject(AuthService);
  private cartService = inject(CartService);

  readonly user$ = this.authService.user$;
  readonly cartCount$ = this.cartService.totalItems$;

  logout() {
    this.authService.logout();
  }

  get isAuthenticated() {
    return this.authService.isAuthenticated;
  }

  get canAccessAdmin(): boolean {
    return this.authService.hasRole('Admin') || 
           this.authService.hasRole('Management') || 
           this.authService.hasRole('Staff');
  }
}
