import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ProductService } from '../../../core/services/product.service';
import { CartService } from '../../../core/services/cart.service';
import { Api } from '../../../core/services/api';
import { Product } from '../../../core/models/product.model';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, ReactiveFormsModule],
  templateUrl: './product-details.html',
  styleUrls: ['./product-details.css'],
})
export class ProductDetails implements OnInit {
  product: Product | null = null;
  brands: any[] = [];
  packagingType: any[] = [];
  quantityForm: FormGroup;
  isLoading = true;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService,
    private cartService: CartService,
    private fb: FormBuilder,
    private api: Api
  ) {
    this.quantityForm = this.fb.group({
      quantity: [1, [Validators.required, Validators.min(1), Validators.max(999)]],
    });
  }

  ngOnInit(): void {
    this.loadProduct();
    this.loadBrands();
    this.loadPackagings();
  }

  get quantity() {
    return this.quantityForm.get('quantity');
  }

  loadBrands(): void {
    this.api.get<any>('/Brands').subscribe(res => this.brands = res.data || []);
  }

  getBrandName(brandId?: number): string {
    if (!brandId) return 'Generic';
    const match = this.brands.find(b => b.id === Number(brandId));
    return match ? match.name : 'Generic / Standard';
  }

  loadPackagings(): void {
    this.api.get<any>('/Packaging').subscribe(res => this.packagingType = res.data || []);
  }

  getPackagingType(id?: number): string {
    if (!id) return 'Retail Standard Box';
    
    // Fallback support for both 'name' and 'type' property variations in your DB
    const match = this.packagingType.find(b => b.id === Number(id));
    return match ? (match.name || match.type) : 'Retail Standard Box';
  }

  loadProduct(): void {
    this.isLoading = true;
    this.error = null;

    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (!id) {
      this.router.navigate(['/products']);
      return;
    }

    this.productService.loadProduct(id).subscribe({
      next: (product) => {
        this.product = product;
      },
      error: () => {
        this.error = 'Failed to load product details';
      },
      complete: () => this.isLoading = false,
    });
  }

  addToCart(): void {
    if (!this.product || this.quantityForm.invalid) return;

    const quantity = Number(this.quantityForm.get('quantity')?.value);
    if (quantity > this.product.stockQty) {
      this.error = `Only ${this.product.stockQty} items available`;
      return;
    }

    this.cartService.addToCart(this.product, quantity);
    this.quantityForm.reset({ quantity: 1 });
  }
}