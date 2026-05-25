import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Category } from '../../../models/category.model';

@Component({
  standalone: true,
  selector: 'app-category-filter',
  imports: [CommonModule],
  templateUrl: './category-filter.html',
  styleUrls: ['./category-filter.css']
})
export class CategoryFilterComponent {
  @Input() categories: Category[] = [];
  @Input() activeCategory: number | null = null;
  @Output() categoryChange = new EventEmitter<number | null>();

  selectCategory(categoryId: number | null): void {
    this.categoryChange.emit(this.activeCategory === categoryId ? null : categoryId);
  }
}
