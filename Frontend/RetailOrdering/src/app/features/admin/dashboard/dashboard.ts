import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AdminService } from '../../../core/services/admin.service';
import {AuthService} from '../../../core/services/auth';
import { InventoryService } from '../inventory.service';
import { Loading } from '../../../core/services/loading';
import { Toast } from '../../../core/services/toast';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.css'],
})
export class Dashboard {
  protected readonly Array = Array; 
  get stats() {
    return this.adminService.stats;
  }

  get products() {
    return this.adminService.products;
  }

  get orders() {
    return this.adminService.orders;
  }

  get users() {
    return this.adminService.users;
  }

  get inventory() {
    return this.inventoryService.inventory;
  }

  constructor(
    private adminService: AdminService,
    private inventoryService: InventoryService,
    private loading: Loading,
    private toast: Toast,
    private authService: AuthService
  ) {
    //this.refresh();
  }

  refresh(): void {
    const isAdmin = this.authService.hasRole('Admin');
    const isManagement = this.authService.hasRole('Management');
    const isStaff = this.authService.hasRole('Staff');

    // FIX: Only load endpoints authorized for the current user role to prevent 403 errors
    if (isAdmin || isManagement || isStaff) {
      this.loading.track(this.adminService.loadOrders()).subscribe({
        error: () => this.toast.show('Unable to load orders', 'danger'),
      });
    }

    if (isAdmin || isManagement) {
      this.loading.track(this.adminService.loadProducts()).subscribe({
        error: () => this.toast.show('Unable to load products', 'danger'),
      });
      this.loading.track(this.inventoryService.loadInventory()).subscribe({
        error: () => this.toast.show('Unable to load inventory', 'danger'),
      });
    }

    if (isAdmin) {
      this.loading.track(this.adminService.loadUsers()).subscribe({
        error: () => this.toast.show('Unable to load users', 'danger'),
      });
    }
  }

  getStatusClass(status: string): string {
    return {
      Pending: 'badge bg-warning text-dark',
      Preparing: 'badge bg-info text-dark',
      Delivered: 'badge bg-success',
      Cancelled: 'badge bg-danger',
    }[status] ?? 'badge bg-secondary';
  }
}
