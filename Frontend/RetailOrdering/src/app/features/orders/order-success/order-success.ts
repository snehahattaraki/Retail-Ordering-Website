import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-order-success',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './order-success.html',
  styleUrls: ['./order-success.css'],
})
export class OrderSuccess {
  get orderId() {
    return Number(this.route.snapshot.queryParamMap.get('orderId')) || null;
  }

  constructor(private route: ActivatedRoute, private router: Router) {}

  backToHistory(): void {
    this.router.navigate(['/orders']);
  }
}
