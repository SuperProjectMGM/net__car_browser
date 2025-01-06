import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { environment } from '../../../environments/environment';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ProfileService } from '../../services/profile.service';
import { VehicleDetail } from '../../models/VehicleDetail.model';
import { VehicleToRentService } from '../../services/VehicleToRent.service';
import { CarsService } from '../../services/cars.service';

@Component({
  selector: 'app-cars-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cars-list.component.html',
  styleUrls: ['./cars-list.component.css'],
})
export class CarsListComponent implements OnInit {
  Math = Math;
  ApiUrl = environment.apiBaseUrl;
  @Input() pickupDateTime: Date | null = null;
  @Input() returnDateTime: Date | null = null;
  currentPage: number = 1;
  carsPerPage: number = 5;
  paginatedCars: VehicleDetail[] = [];
  allCars: VehicleDetail[] = [];
  isLoading: boolean = true;

  constructor(
    private profileService: ProfileService,
    private authService: AuthService,
    private router: Router,
    private vehicleToRentService: VehicleToRentService,
    private carsService: CarsService
  ) {}

  ngOnInit() {
    // Subscribe to filtered cars from CarsService
    this.carsService.getFilteredCars().subscribe((cars) => {
      this.allCars = cars;
      this.isLoading = false; // Loading is finished when cars are available
      this.updatePaginatedCars(); // Update the paginated list
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['pickupDateTime'] || changes['returnDateTime']) {
      this.updatePaginatedCars();
    }
  }

  viewDeal(car: VehicleDetail): void {
    this.vehicleToRentService.setCar(car);
    this.router.navigate(['/view-deals']);
  }

  updatePaginatedCars() {
    if (!this.allCars || this.allCars.length === 0) {
      console.warn('No cars available to display.');
      this.paginatedCars = [];
      return;
    }

    const startIndex = (this.currentPage - 1) * this.carsPerPage;
    const endIndex = startIndex + this.carsPerPage;

    this.paginatedCars = this.allCars.slice(startIndex, endIndex);
    console.log('Cars on the current page:', this.paginatedCars);
  }

  goToPage(page: number) {
    if (page < 1 || page > Math.ceil(this.allCars.length / this.carsPerPage)) {
      console.warn('Invalid page number:', page);
      return;
    }
    console.log('Navigating to page:', page);
    this.currentPage = page;
    this.updatePaginatedCars();
  }
}
