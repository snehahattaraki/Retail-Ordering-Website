import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Observable, combineLatest, debounceTime, distinctUntilChanged, map, startWith } from 'rxjs';
import { Product } from '../../../models/product.model';
import { Brand } from '../../../models/brand.model';
import { Category } from '../../../models/category.model';
import { ProductsService } from '../../../services/products.service';
import { CategoriesService } from '../../../services/categories.service';
import { BrandsService } from '../../../services/brands.service';
import { CartService } from '../../../services/cart.service';
import { ToastService } from '../../../services/toast.service';
import { CategoryFilterComponent } from '../category-filter/category-filter';
import { BrandFilterComponent } from '../brand-filter/brand-filter';
import { ProductCardComponent } from '../../../shared/components/product-card/product-card';

@Component({
  standalone: true,
  selector: 'app-product-list',
  imports: [CommonModule, ReactiveFormsModule, RouterLink, CategoryFilterComponent, BrandFilterComponent, ProductCardComponent],
  templateUrl: './product-list.html',
  styleUrls: ['./product-list.css'],
})
export class ProductListComponent {
  readonly searchControl = new FormControl('');
  readonly categoryControl = new FormControl<number | null>(null);
  readonly brandControl = new FormControl<number | null>(null);

  private loadingSignal = signal(true);
  readonly loading = this.loadingSignal.asReadonly();

  readonly products$!: Observable<Product[]>;
  readonly categories$!: Observable<Category[]>;
  readonly brands$!: Observable<Brand[]>;

  constructor(
    private productsService: ProductsService,
    private categoriesService: CategoriesService,
    private brandsService: BrandsService,
    private cartService: CartService,
    private toastService: ToastService
  ) {
    this.products$ = combineLatest([
      this.productsService.getProducts(),
      this.searchControl.valueChanges.pipe(startWith(''), debounceTime(180), distinctUntilChanged()),
      this.categoryControl.valueChanges.pipe(startWith(null)),
      this.brandControl.valueChanges.pipe(startWith(null)),
    ]).pipe(
      map(([products, search, categoryId, brandId]) => {
        const query = (search || '').trim().toLowerCase();
        return products.filter((product) => {
          const matchesSearch = !query || product.name.toLowerCase().includes(query);
          const matchesCategory = !categoryId || product.categoryId === categoryId;
          const matchesBrand = !brandId || product.brandId === brandId;
          return matchesSearch && matchesCategory && matchesBrand;
        });
      })
    );

    this.categories$ = this.categoriesService.getCategories();
    this.brands$ = this.brandsService.getBrands();
    this.productsService.getProducts().subscribe(() => this.loadingSignal.set(false));
  }

  onAddToCart(product: Product): void {
    this.cartService.addToCart(product);
    this.toastService.show(`${product.name} added to cart.`);
  }

  onCategoryChange(categoryId: number | null): void {
    this.categoryControl.setValue(categoryId, { emitEvent: true });
  }

  onBrandChange(brandId: number | null): void {
    this.brandControl.setValue(brandId, { emitEvent: true });
  }
}
