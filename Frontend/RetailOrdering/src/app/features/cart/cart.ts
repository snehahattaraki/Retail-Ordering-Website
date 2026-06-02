import { Injectable, signal } from '@angular/core';

export interface CartItem {
  productId: number;
  quantity: number;
}

@Injectable({
  providedIn: 'root',
})
export class Cart {
  items = signal<CartItem[]>([]);

  add(item: CartItem) {
    this.items.update((list) => {
      const idx = list.findIndex((i) => i.productId === item.productId);
      if (idx === -1) return [...list, item];
      const copy = [...list];
      copy[idx] = { ...copy[idx], quantity: copy[idx].quantity + item.quantity };
      return copy;
    });
  }

  remove(productId: number) {
    this.items.update((list) => list.filter((i) => i.productId !== productId));
  }

  clear() {
    this.items.set([]);
  }
}
