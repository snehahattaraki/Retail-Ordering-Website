import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterModule } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class NavbarComponent {
  private readonly auth = inject(AuthService);
  readonly isAuthenticated$ = this.auth.isAuthenticated$;

  get isAdmin(): boolean {
    return this.auth.isAdmin;
  }

  logout(): void {
    this.auth.logout();
  }
}
