import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InventoryService, InventoryItem } from '../inventory.service';
import { Loading } from '../../../core/services/loading';
import { Toast } from '../../../core/services/toast';

@Component({
  selector: 'app-inventory',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './inventory.html',
  styleUrls: ['./inventory.css'],
})
export class Inventory {
  get inventory() {
    return this.inventoryService.inventory;
  }

  stockMap: Record<number, number> = {};

  constructor(
    private inventoryService: InventoryService,
    private loading: Loading,
    private toast: Toast
  ) {
    this.loadInventory();
  }

  loadInventory(): void {
    this.stockMap = {};
    this.loading.track(this.inventoryService.loadInventory()).subscribe({
      next: () => {
        this.inventory().forEach((item) => (this.stockMap[item.productId] = item.stockQty));
      },
      error: () => this.toast.show('Unable to load inventory', 'danger'),
    });
  }

  saveStock(item: InventoryItem): void {
    const stockQty = this.stockMap[item.productId] ?? item.stockQty;
    this.loading.track(this.inventoryService.updateStock(item.productId, stockQty)).subscribe(() => {
      this.toast.show(`Stock updated for product ${item.productId}`, 'success');
      this.loadInventory();
    });
  }
}
