import { Injectable, signal, computed } from '@angular/core';
import { catchError, map, Observable, of, tap } from 'rxjs';
import { Api } from '../../core/services/api';
import { API_ENDPOINTS } from '../../core/constants/api.constants';
import { ApiResponse } from '../../core/models/api-response.model';
import { AuthService } from './auth'; // INJECTED AUTH SERVICE

export interface Product {
  id: number;
  name: string;
  category: string;
  brandId?: number;     // ADDED
  brandName?: string;   // ADDED
  price: number;
  stockQty: number;
  imageUrl?: string;
}

export interface AdminBrand {
  id: number;
  name: string;
}

export interface User {
  id: number;
  name: string;
  email: string;
  role: string;
}

export interface AdminOrder {
  id: number;
  userId: number;
  totalAmount: number;
  status: string;
}

export interface DashboardStats {
  totalProducts: number;
  totalUsers: number;
  totalOrders: number;
  totalRevenue: number;
}

// ADDED: Category Interface
export interface AdminCategory {
  id: number;
  name: string;
}

export interface AdminRoles {
  id: number;
  name: string;
}

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  products = signal<Product[]>([]);
  users = signal<User[]>([]);
  orders = signal<AdminOrder[]>([]);
  categories = signal<AdminCategory[]>([]); // ADDED: Categories Signal
  roles = signal<AdminRoles[]>([]); // ADDED: Roles Signal
  brands = signal<AdminBrand[]>([]);
  stats = computed<DashboardStats>(() => {
    return {
      totalProducts: this.products().length,
      totalUsers: this.users().length,
      totalOrders: this.orders().length,
      totalRevenue: this.orders().reduce((sum, order) => sum + (order.totalAmount || 0), 0)
    };
  });

  constructor(private api: Api, private authService: AuthService) {
    //this.loadProducts().subscribe();
    //this.loadUsers().subscribe();
    //this.loadOrders().subscribe();
    //this.loadStats().subscribe();
    //this.loadCategories().subscribe(); // ADDED: Auto-fetch on load
    //this.loadRoles().subscribe(); // ADDED: Auto-fetch roles on load
    this.authService.user$.subscribe(user => {
      if (user) {
        const isAdmin = this.authService.hasRole('Admin');
        const isManagement = this.authService.hasRole('Management');
        const isStaff = this.authService.hasRole('Staff');

        // 1. Everyone can see Orders
        if (isAdmin || isManagement || isStaff) {
          this.loadOrders().subscribe();
        }

        // 2. Admin & Management can see Products, Categories, and Stats
        if (isAdmin || isManagement) {
          this.loadProducts().subscribe();
          this.loadCategories().subscribe();
          //this.loadStats().subscribe();
          this.loadBrands().subscribe();
        }

        // 3. ONLY Admins can see Users
        if (isAdmin) {
          this.loadUsers().subscribe();
          this.loadRoles().subscribe(); // ADDED: Fetch roles only for Admins
        }
      } else {
        // Clear all data if they log out
        this.products.set([]);
        this.users.set([]);
        this.orders.set([]);
        this.categories.set([]);
        //this.stats.set({ totalProducts: 0, totalUsers: 0, totalOrders: 0, totalRevenue: 0 });
      }
    });
  }

  loadBrands(): Observable<AdminBrand[]> {
    // FIX: Prepend /api/ to match the C# controller routing rule
    return this.api.get<ApiResponse<AdminBrand[]>>(API_ENDPOINTS.BRANDS).pipe(
      map((res) => res.data ?? []),
      catchError(() => of([] as AdminBrand[])),
      tap((brands) => this.brands.set(brands))
    );
  }

  saveBrand(brand: AdminBrand): Observable<AdminBrand> {
    // FIX: Prepend /api/ to both put and post route paths
    const action = brand.id 
      ? this.api.put<ApiResponse<AdminBrand>>(`${API_ENDPOINTS.BRANDS}/${brand.id}`, brand) 
      : this.api.post<ApiResponse<AdminBrand>>(API_ENDPOINTS.BRANDS, brand);
      
    return action.pipe(
      map((res) => res.data ?? brand), 
      tap(() => this.loadBrands().subscribe())
    );
  }

  deleteBrand(id: number): Observable<any> {
    // FIX: Prepend /api/ to the delete request path target
    return this.api.delete<ApiResponse<any>>(`${API_ENDPOINTS.BRANDS}/${id}`).pipe(
      tap(() => this.loadBrands().subscribe())
    );
  }

  loadProducts(): Observable<Product[]> {
    return this.api.get<ApiResponse<Product[]>>(API_ENDPOINTS.INVENTORY).pipe(
      map((res) => res.data ?? []),
      catchError(() => of([] as Product[])),
      tap((products) => this.products.set(products))
    );
  }

  saveProduct(product: Product): Observable<Product> {
    const action = product.id
      ? this.api.put<ApiResponse<Product>>(`${API_ENDPOINTS.INVENTORY}/${product.id}`, product)
      : this.api.post<ApiResponse<Product>>(API_ENDPOINTS.INVENTORY, product);

    return action.pipe(
      map((res) => res.data ?? product),
      catchError(() => of(product)),
      tap(() => this.loadProducts().subscribe())
    );
  }

  deleteProduct(id: number): Observable<any> {
    return this.api.delete<ApiResponse<any>>(`${API_ENDPOINTS.INVENTORY}/${id}`).pipe(
      map((res) => res.data),
      catchError(() => of(null)),
      tap(() => this.loadProducts().subscribe())
    );
  }

  loadRoles(): Observable<AdminRoles[]> {
    return this.api.get<ApiResponse<AdminRoles[]>>(`${API_ENDPOINTS.ADMIN}/roles`).pipe(
      map((res) => res.data ?? []),
      catchError(() => of([] as AdminRoles[])),
      tap((roles) => this.roles.set(roles))
    );
  }

  loadUsers(): Observable<User[]> {
    return this.api.get<ApiResponse<User[]>>(`${API_ENDPOINTS.ADMIN}/users`).pipe(
      map((res) => res.data ?? []),
      catchError(() => of([] as User[])),
      tap((users) => this.users.set(users))
    );
  }

  saveUser(user: User): Observable<User> {
    const action = user.id
      ? this.api.put<ApiResponse<User>>(`${API_ENDPOINTS.ADMIN}/users/${user.id}`, user)
      : this.api.post<ApiResponse<User>>(`${API_ENDPOINTS.ADMIN}/users`, user);

    return action.pipe(
      map((res) => res.data ?? user),
      catchError(() => of(user)),
      tap(() => this.loadUsers().subscribe())
    );
  }

  deleteUser(id: number): Observable<any> {
    return this.api.delete<ApiResponse<any>>(`${API_ENDPOINTS.ADMIN}/users/${id}`).pipe(
      map((res) => res.data),
      catchError(() => of(null)),
      tap(() => this.loadUsers().subscribe())
    );
  }

  loadOrders(): Observable<AdminOrder[]> {
    return this.api.get<ApiResponse<any[]>>(`${API_ENDPOINTS.ADMIN}/orders`).pipe(
      map((res) => {
        return (res.data ?? []).map(o => ({
          ...o,
          id: o.id,
          totalAmount: o.totalAmount || o.total
        }));
      }),
      catchError(() => of([] as AdminOrder[])),
      tap((orders) => this.orders.set(orders))
    );
  }

  updateOrderStatus(id: number, status: string): Observable<AdminOrder> {
    return this.api.put<ApiResponse<AdminOrder>>(`${API_ENDPOINTS.ADMIN}/orders/${id}/status`, { status }).pipe(
      map((res) => res.data ?? ({ id, userId: 0, totalAmount: 0, status } as AdminOrder)),
      catchError(() => of({ id, userId: 0, totalAmount: 0, status } as AdminOrder)),
      tap(() => this.loadOrders().subscribe())
    );
  }

  // loadStats(): Observable<DashboardStats> {
  //   return this.api.get<ApiResponse<DashboardStats>>(`${API_ENDPOINTS.ADMIN}/stats`).pipe(
  //     map((res) => res.data ?? { totalProducts: this.products().length, totalUsers: this.users().length, totalOrders: this.orders().length, totalRevenue: this.orders().reduce((sum, order) => sum + (order.totalAmount || 0), 0) }),
  //     catchError(() => of({ totalProducts: this.products().length, totalUsers: this.users().length, totalOrders: this.orders().length, totalRevenue: this.orders().reduce((sum, order) => sum + (order.totalAmount || 0), 0) } as DashboardStats)),
  //     tap((stats) => this.stats.set(stats))
  //   );
  // }

  // ADDED: Fetch from your backend CategoriesController
  loadCategories(): Observable<AdminCategory[]> {
    return this.api.get<ApiResponse<AdminCategory[]>>(`${API_ENDPOINTS.CATEGORIES}`).pipe(
      map((res) => res.data ?? []),
      catchError(() => of([] as AdminCategory[])),
      tap((categories) => this.categories.set(categories))
    );
  }
  saveCategory(category: AdminCategory): Observable<AdminCategory> {
    const action = category.id && category.id !== 0
      ? this.api.put<ApiResponse<AdminCategory>>(`${API_ENDPOINTS.CATEGORIES}/${category.id}`, category)
      : this.api.post<ApiResponse<AdminCategory>>(API_ENDPOINTS.CATEGORIES, category);

    return action.pipe(
      map((res) => res.data ?? category),
      tap(() => {
        this.loadCategories().subscribe(); // Refresh categories
        this.loadProducts().subscribe(); // Refresh products in case category names changed
      })
    );
  }

  deleteCategory(id: number): Observable<any> {
    return this.api.delete<ApiResponse<any>>(`${API_ENDPOINTS.CATEGORIES}/${id}`).pipe(
      map((res) => res.data),
      tap(() => this.loadCategories().subscribe())
    );
  }
}