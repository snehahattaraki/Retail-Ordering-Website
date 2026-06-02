import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService, AuthUser } from '../../core/services/auth';
import { Api } from '../../core/services/api';
import { API_ENDPOINTS } from '../../core/constants/api.constants';
import { ApiResponse } from '../../core/models/api-response.model';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './profile.html',
  styleUrls: ['./profile.css']
})
export class Profile implements OnInit {
  user: AuthUser | null = null;
  stats = { totalOrders: 0 };

  constructor(private authService: AuthService, private api: Api) {}

  ngOnInit(): void {
    this.user = this.authService.user;
    
    // Fetch the user's orders to calculate their stats
    this.api.get<ApiResponse<any[]>>(API_ENDPOINTS.ORDERS).subscribe({
      next: (res) => {
        const orders = res.data ?? [];
        this.stats.totalOrders = orders.length;
      }
    });
  }

  logout(): void {
    this.authService.logout();
  }
}