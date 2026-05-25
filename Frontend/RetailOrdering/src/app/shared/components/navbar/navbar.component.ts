import { Component, inject } from '@angular/core';
import { NgIf } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../../core/services/auth';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [NgIf, RouterLink, RouterLinkActive],
  templateUrl: './navbar.html',
  styleUrls: ['./navbar.css'],
})
export class NavbarComponent {
  auth = inject(AuthService);

  logout() {
    this.auth.logout();
  }

  get isAuthenticated() {
    return this.auth.isAuthenticated;
  }

  get isAdmin() {
    return this.auth.isAdmin;
  }
}
