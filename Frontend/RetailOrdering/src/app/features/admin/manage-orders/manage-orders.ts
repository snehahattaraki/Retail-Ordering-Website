import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminService, AdminOrder } from '../admin.service';
import { Loading } from '../../../core/services/loading';
import { Toast } from '../../../core/services/toast';

@Component({
  selector: 'app-manage-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './manage-orders.html',
  styleUrls: ['./manage-orders.css'],
})
export class ManageOrders {
  get orders() {
    return this.adminService.orders;
  }

  statuses = ['Pending', 'Preparing', 'Delivered', 'Cancelled'];

  constructor(
    private adminService: AdminService,
    private loading: Loading,
    private toast: Toast
  ) {}

  updateStatus(order: AdminOrder, status: string): void {
    this.loading.track(this.adminService.updateOrderStatus(order.orderId, status)).subscribe(() => {
      this.toast.show(`Order #${order.orderId} moved to ${status}`, 'success');
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
