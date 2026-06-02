import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { OrderService } from '../../../core/services/order.service';
import { Loading } from '../../../core/services/loading';
import { Toast } from '../../../core/services/toast';
import { Order } from '../../../core/models/order.model';

@Component({
  selector: 'app-order-history',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './order-history.html',
  styleUrls: ['./order-history.css'],
})
export class OrderHistory {
  orders: Order[] = [];

  constructor(
    private orderService: OrderService,
    private loading: Loading,
    private toast: Toast,
    private router: Router
  ) {
    this.loadOrders();
  }

  get totalOrders(): number {
    return this.orders.length;
  }

  get totalSpent():number{
    return this.orders.reduce((total, order) => total + order.totalAmount, 0);
  }

  get latestOrder(): Order | null {
    return this.orders.length ? this.orders[0] : null;
  }

  getStatusClass(status: string): string {
    return {
      Pending: 'badge bg-warning text-dark',
      Preparing: 'badge bg-info text-dark',
      Delivered: 'badge bg-success',
      Cancelled: 'badge bg-danger',
    }[status] ?? 'badge bg-secondary';
  }

  loadOrders(): void {
    this.loading.track(this.orderService.loadOrders()).subscribe({
      next: (orders) => (this.orders = orders),
      error: () => this.toast.show('Unable to load orders', 'danger'),
    });
  }

  reorder(order: Order): void {
    this.loading.track(this.orderService.reorder(order.id)).subscribe({
      next: () => {
        this.toast.show('Reorder placed successfully', 'success');
        // Navigate to the newly registered success page
        this.router.navigate(['/orders/success']);
      },
      error: () => {
        this.toast.show('Failed to place reorder. Please try again.', 'danger');
      }
    });
  }
}
