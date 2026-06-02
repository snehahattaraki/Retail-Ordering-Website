import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { CategoryService } from '../../../core/services/category.service';
import { CartService } from '../../../core/services/cart.service';
import { ProductService } from '../../../core/services/product.service';
import { Category } from '../../../core/models/category.model';
import { Product } from '../../../core/models/product.model';
import { Pagination } from '../../../core/models/pagination.model';
import {Api} from '../../../core/services/api';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './product-list.html',
  styleUrls: ['./product-list.css'],
})
export class ProductList implements OnInit {
  products: Product[] = [];
  categories: Category[] = [];
  search = '';
  categoryFilter: number | '' = '';
  brandFilter: number | '' = '';
  brands: any[] = [];
  sortBy = 'popular';
  currentPage = 1;
  pageSize = 8;
  isLoadingProducts = false;
  isLoadingCategories = false;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private cartService: CartService,
    private api: Api
  ) {}

  ngOnInit(): void {
    this.loadAllProducts();
    this.loadCategories();
    this.loadBrands();
  }

  loadAllProducts(): void {
    this.isLoadingProducts = true;
    this.productService.loadProducts().subscribe({
      next: (pagination: Pagination<Product>) => {
        // Verify items is an array
        if (Array.isArray(pagination.items)) {
          this.products = pagination.items;
          //console.log('✓ Products loaded:', this.products.length, 'items');
        } else {
          //console.error('✗ Pagination items is not an array:', pagination.items);
          this.products = [];
        }
      },
      error: (err) => {
        //console.error('✗ Error loading products:', err);
        this.products = [];
      },
      complete: () => {
        this.isLoadingProducts = false;
      },
    });
  }

  loadCategories(): void {
    this.isLoadingCategories = true;
    this.categoryService.loadCategories().subscribe({
      next: (categories: Category[]) => {
        // Verify categories is an array
        if (Array.isArray(categories)) {
          this.categories = categories;
          //console.log('✓ Categories loaded:', this.categories.length, 'items');
        } else {
          //console.error('✗ Categories response is not an array:', categories);
          this.categories = [];
        }
      },
      error: (err) => {
        //console.error('✗ Error loading categories:', err);
        this.categories = [];
      },
      complete: () => {
        this.isLoadingCategories = false;
      },
    });
  }

  // ADDED: Load brand data records asynchronously from BrandsController
  loadBrands(): void {
    this.api.get<any>('/Brands').subscribe({
      next: (res) => {
        this.brands = res.data || [];
        //console.log('✓ Storefront Brands loaded:', this.brands.length, 'items');
      },
      error: (err) => {
        //console.error('✗ Error loading storefront brands:', err);
      }
    });
  }

  getBrandName(id: number): string {
    if (!id) return 'Generic';
    const match = this.brands.find(b => b.id === Number(id));
    return match ? match.name : 'Generic';
  }

  get filteredProducts(): Product[] {
    return this.products
      .filter((product) => {
        // FIX: Compare the product's categoryId to the selected category ID
        const filterId = this.categoryFilter ? Number(this.categoryFilter) : null;
        const matchesCategory = filterId ? product.categoryId === filterId : true;
        
        const matchesBrand = this.brandFilter ? (product as any).brandId === Number(this.brandFilter) : true;
        const matchesSearch = this.search ? product.name.toLowerCase().includes(this.search.toLowerCase()) : true;
        return matchesCategory && matchesBrand && matchesSearch;
      })
      .sort((a, b) => {
        if (this.sortBy === 'low') return a.price - b.price;
        if (this.sortBy === 'high') return b.price - a.price;
        return b.stockQty - a.stockQty;
      });
  }

  get pageCount(): number {
    return Math.max(1, Math.ceil(this.filteredProducts.length / this.pageSize));
  }

  get pagedProducts(): Product[] {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.filteredProducts.slice(start, start + this.pageSize);
  }

  onPage(page: number): void {
    this.currentPage = page;
  }

  addToCart(product: Product): void {
    this.cartService.addToCart(product);
  }
}
