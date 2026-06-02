import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Product } from '../../../features/products/products';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-card.html',
  styleUrls: ['./product-card.css'],
})
export class ProductCard {
  @Input() product: Product | null = null;
  @Output() addToCart = new EventEmitter<Product>();

  onAddToCart() {
    if (this.product) {
      this.addToCart.emit(this.product);
    }
  }
}
