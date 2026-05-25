import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Observable, combineLatest, map } from 'rxjs';
import { CartItem } from '../../../models/cart-item.model';
import { Product } from '../../../models/product.model';
import { CartService } from '../../../services/cart.service';
import { ProductsService } from '../../../services/products.service';

@Component({
  standalone: true,
  selector: 'app-cart-page',
  imports: [CommonModule, RouterLink],
  templateUrl: './cart-page.html',
  styleUrls: ['./cart-page.css'],
})
export class CartPageComponent {
  readonly cartItems$!: Observable<(CartItem & { product?: Product; totalPrice: number })[]>;
  readonly total$!: Observable<number>;

  constructor(private cartService: CartService, private productsService: ProductsService) {
    this.cartItems$ = combineLatest([
      this.cartService.items$,
      this.productsService.getProducts(),
    ]).pipe(
      map(([items, products]) =>
        items.map((item) => {
          const product = products.find((p) => p.productId === item.productId);
          return {
            ...item,
            product,
            totalPrice: product ? product.price * item.quantity : 0,
          };
        })
      )
    );

    this.total$ = this.cartItems$.pipe(
      map((items) => items.reduce((sum, item) => sum + item.totalPrice, 0))
    );
  }

  increase(item: CartItem): void {
    this.cartService.updateQuantity(item.productId, item.quantity + 1);
  }

  decrease(item: CartItem): void {
    this.cartService.updateQuantity(item.productId, Math.max(1, item.quantity - 1));
  }

  remove(productId: number): void {
    this.cartService.removeItem(productId);
  }
}
