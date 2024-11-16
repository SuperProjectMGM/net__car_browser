import { Component } from '@angular/core';
import { FiltersComponent } from '../filters/filters.component';
import { CarsListComponent } from '../cars-list/cars-list.component';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [FiltersComponent, CarsListComponent, CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent {
  isLoading: boolean = false;

  constructor(/*private authService: AuthService*/ private router: Router) {}

  goToProfile() {
    this.isLoading = true;
    this.router.navigate(['/profile']);
    this.isLoading = false;
  }
}
