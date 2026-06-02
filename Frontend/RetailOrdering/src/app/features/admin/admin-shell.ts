import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../core/services/auth';

@Component({
  selector: 'app-admin-shell',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './admin-shell.html'
})
export class AdminShell implements OnInit {
  userRole: string = 'Staff'; // Default to lowest privilege 

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    // Reactively subscribe to the auth state to get the correct role
    this.authService.user$.subscribe(user => {
      if (user && user.roles && user.roles.length > 0) {
        // Ensure the role string matches our expected casing (Admin, Management, Staff)
        const role = user.roles[0];
        this.userRole = role.charAt(0).toUpperCase() + role.slice(1).toLowerCase();
        const currentUrl = this.router.url;

        if (this.userRole === 'Staff') {
          // If Staff lands on the Dashboard or any other page, force them to Orders
          if (currentUrl !== '/admin/orders') {
            this.router.navigate(['/admin/orders']);
          }
        } 
        else if (this.userRole === 'Management') {
          // If Management tries to snoop in the Users/HR tab, kick them to Dashboard
          if (currentUrl.includes('/admin/users')) {
            this.router.navigate(['/admin']);
          }
        }
      }
    });
  }

  logout(): void {
    this.authService.logout();
    // The router.navigate(['/login']) is already handled inside your AuthService logout method!
  }
}