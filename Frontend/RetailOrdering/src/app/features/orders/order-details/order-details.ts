import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { OrderService } from '../../../core/services/order.service';
import { Loading } from '../../../core/services/loading';
import { Toast } from '../../../core/services/toast';
import { Order } from '../../../core/models/order.model';
// import { Api } from '../../../core/services/api';
// import { API_ENDPOINTS } from '../../../core/constants/api.constants';
// import { ApiResponse } from '../../../core/models/api-response.model';

@Component({
  selector: 'app-order-details',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './order-details.html',
  styleUrls: ['./order-details.css'],
})
export class OrderDetails {
  order: any = null;
  items = 0;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private orderService: OrderService,
    private loading: Loading,
    private toast: Toast
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (!id) {
      this.router.navigate(['/orders']);
      return;
    }
    this.loadOrder(id);
  }

  /*loadOrder(orderId: number): void {
    this.loading.track(this.orderService.loadOrder(orderId)).subscribe({
      next: (order) => {
        this.order = order;
        this.items = order.orderItems.reduce((sum, item) => sum + item.quantity, 0);
      },
      error: () => this.toast.show('Unable to load order details', 'danger'),
    });
  }*/
 loadOrder(orderId: number): void {
    this.loading.track(this.orderService.loadOrder(orderId)).subscribe({
      next: (order: any) => {
        this.order = order;
        // Safely check both property names to prevent errors
        const orderItemsArray = order.items || order.orderItems || [];
        this.items = orderItemsArray.reduce((sum: number, item: any) => sum + item.quantity, 0);
      },
      error: () => this.toast.show('Unable to load order details', 'danger'),
    });
  }

  printInvoice(): void {
    window.print();
  }

  getStatusClass(status: string): string {
    return {
      Pending: 'badge bg-warning text-dark',
      Preparing: 'badge bg-info text-dark',
      Delivered: 'badge bg-success',
      Cancelled: 'badge bg-danger',
    }[status] ?? 'badge bg-secondary';
  }
  get progressWidth(): string {
    const status = this.order?.status || 'Pending';
    if (status === 'Cancelled') return '100%';
    if (status === 'Pending') return '25%';
    if (status === 'Preparing') return '50%';
    if (status === 'Out for Delivery' || status === 'Shipped') return '75%';
    if (status === 'Delivered') return '100%';
    return '0%';
  }
}
