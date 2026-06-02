import { Injectable } from '@angular/core';
import { OrdersService } from './orders.service';

@Injectable({
  providedIn: 'root',
})
export class Orders {
  constructor(private svc: OrdersService) {}

  get orders() {
    return this.svc.orders;
  }

  loadOrders() {
    return this.svc.loadOrders();
  }
}
