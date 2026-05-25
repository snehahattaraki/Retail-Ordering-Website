import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Brand } from '../../../models/brand.model';

@Component({
  standalone: true,
  selector: 'app-brand-filter',
  imports: [CommonModule],
  templateUrl: './brand-filter.html',
  styleUrls: ['./brand-filter.css']
})
export class BrandFilterComponent {
  @Input() brands: Brand[] = [];
  @Input() activeBrand: number | null = null;
  @Output() brandChange = new EventEmitter<number | null>();

  selectBrand(brandId: number | null): void {
    this.brandChange.emit(this.activeBrand === brandId ? null : brandId);
  }
}
