import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Sidebar } from '../../shared/components/sidebar/sidebar';

@Component({
  selector: 'app-admin-shell',
  standalone: true,
  imports: [CommonModule, RouterOutlet, Sidebar],
  templateUrl: './admin-shell.html',
  styleUrls: ['./admin-shell.css'],
})
export class AdminShell {}
