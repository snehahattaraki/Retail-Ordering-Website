import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../core/services/auth';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './home.html',
  styleUrls: ['./home.css'],
})
export class Home {
  readonly auth = inject(AuthService);

  get displayName() {
    return this.auth.user?.email || 'Food lover';
  }

  get isAdmin() {
    return this.auth.isAdmin;
  }
}
