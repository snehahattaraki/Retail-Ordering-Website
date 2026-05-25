import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AdminService } from '../admin.service';
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
    private toast: Toast
  ) {
    this.refresh();
  }

  refresh(): void {
    this.loading.track(this.adminService.loadStats()).subscribe({
      error: () => this.toast.show('Unable to load dashboard stats', 'danger'),
    });
    this.loading.track(this.adminService.loadProducts()).subscribe({
      error: () => this.toast.show('Unable to load products', 'danger'),
    });
    this.loading.track(this.adminService.loadOrders()).subscribe({
      error: () => this.toast.show('Unable to load orders', 'danger'),
    });
    this.loading.track(this.adminService.loadUsers()).subscribe({
      error: () => this.toast.show('Unable to load users', 'danger'),
    });
    this.loading.track(this.inventoryService.loadInventory()).subscribe({
      error: () => this.toast.show('Unable to load inventory', 'danger'),
    });
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
