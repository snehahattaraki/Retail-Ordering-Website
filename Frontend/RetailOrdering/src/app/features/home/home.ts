import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { CategoryService } from '../../core/services/category.service';
import { ProductService } from '../../core/services/product.service';
import { CartService } from '../../core/services/cart.service';
import { Api } from '../../core/services/api'; 

import { Category } from '../../core/models/category.model';
import { Product } from '../../core/models/product.model';
import { Pagination } from '../../core/models/pagination.model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './home.html',
  styleUrls: ['./home.css'],
})
export class Home implements OnInit {
  categories: Category[] = [];
  featuredProducts: Product[] = [];
  brands: any[] = []; 
  isLoadingCategories = false;
  isLoadingProducts = false;
  categoryError: string | null = null;
  productError: string | null = null;

  search = '';
  selectedCategory: number | '' = ''; 

  constructor(
    private categoryService: CategoryService,
    private productService: ProductService,
    private cartService: CartService,
    private api: Api 
  ) {}

  ngOnInit(): void {
    this.loadCategories();
    this.loadFeaturedProducts();
    this.loadBrands(); 
  }

  loadBrands(): void {
    this.api.get<any>('/Brands').subscribe({
      next: (res) => this.brands = res.data || []
    });
  }

  getBrandName(brandId?: number): string {
    if (!brandId) return 'Generic';
    const match = this.brands.find(b => b.id === Number(brandId));
    return match ? match.name : 'Generic';
  }

  loadCategories(): void {
    this.isLoadingCategories = true;
    this.categoryError = null;

    this.categoryService.loadCategories().subscribe({
      next: (categories: Category[]) => {
        if (Array.isArray(categories)) {
          this.categories = categories;
        } else {
          this.categories = [];
          this.categoryError = 'Invalid categories format received';
        }
      },
      error: () => {
        this.categoryError = 'Failed to load categories';
        this.categories = [];
      },
      complete: () => this.isLoadingCategories = false,
    });
  }

  loadFeaturedProducts(): void {
    this.isLoadingProducts = true;
    this.productError = null;

    this.productService.loadProducts().subscribe({
      next: (pagination: Pagination<Product>) => {
        if (Array.isArray(pagination.items)) {
          this.featuredProducts = pagination.items;
        } else {
          this.featuredProducts = [];
          this.productError = 'Invalid products format received';
        }
      },
      error: () => {
        this.productError = 'Failed to load products';
        this.featuredProducts = [];
      },
      complete: () => this.isLoadingProducts = false,
    });
  }

  get filteredProducts(): Product[] {
    if (!Array.isArray(this.featuredProducts)) return [];
    
    return this.featuredProducts.filter((product) => {
      const matchesCategory = this.selectedCategory
        ? product.categoryId === Number(this.selectedCategory)
        : true;
      const matchesSearch = this.search
        ? product.name.toLowerCase().includes(this.search.toLowerCase())
        : true;
      return matchesCategory && matchesSearch;
    });
  }

  addToCart(product: Product): void {
    this.cartService.addToCart(product, 1);
  }
}