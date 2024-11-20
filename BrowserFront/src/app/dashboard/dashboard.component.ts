import { Component } from '@angular/core';
import { CarsService } from '../services/cars.service';
import {
  CarsListComponent,
  VehicleDetail,
} from '../cars-list/cars-list.component';
import { CommonModule, Time } from '@angular/common';
import { Router } from '@angular/router';
import { FiltersComponent } from '../filters/filters.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CarsListComponent,
    CommonModule,
    FiltersComponent,
    FormsModule,
    HttpClientModule,
  ],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent {
  pickupLocation: string = '';
  isLoading: boolean = false;
  pickupDateTime: Date | null = null;
  returnDateTime: Date | null = null;
  availableCars: VehicleDetail[] = [];
  filteredCars: VehicleDetail[] = [];
  pickupDateTimeToreserve: Date | null = null;
  returnDateTimeToreserve: Date | null = null;
  constructor(
    private carsService: CarsService,
    private router: Router,
    private authService: AuthService
  ) {
    console.log('HttpClient is ready');
  }

  onSearch() {
    this.availableCars = [];
    if (this.pickupDateTime && this.returnDateTime && this.pickupLocation) {
      const pickupDate = new Date(this.pickupDateTime); // Konwersja string -> Date
      const returnDate = new Date(this.returnDateTime);

      if (isNaN(pickupDate.getTime()) || isNaN(returnDate.getTime())) {
        console.error('Invalid date format');
        return;
      }

      this.isLoading = true;

      this.carsService
        .searchCars(pickupDate, returnDate, this.pickupLocation)
        .subscribe(
          (cars) => {
            this.isLoading = false;
            this.availableCars = cars; // Przechowujemy dostępne samochody
            this.filteredCars = cars;
          },
          (error) => {
            this.isLoading = false;
            console.error('Wystąpił błąd przy wyszukiwaniu samochodów:', error);
          }
        );
    } else {
      console.error('Both pickup and return dates must be set.');
    }
    this.pickupDateTimeToreserve = this.pickupDateTime;
    this.returnDateTimeToreserve = this.returnDateTime;
    this.pickupLocation = '';
    this.pickupDateTime = null;
    this.returnDateTime = null;
  }

  goToProfile() {
    console.log('sprawdzamy stan logowania', this.authService.isLoggedIn());
    if (this.authService.isLoggedIn()) {
      console.log('chcemy isc do profilu');
      this.router.navigate(['/profile']);
    } else {
      this.router.navigate(['/login']);
    }
  }

  combineDateAndTimeObjects(date: Date, time: Time): Date {
    if (!time || time.hours === null || time.minutes === null) {
      throw new Error('Invalid Time object');
    }
    const combinedDate = new Date(date);
    combinedDate.setHours(
      parseInt(String(time.hours), 10),
      parseInt(String(time.minutes), 10),
      0
    );
    return combinedDate;
  }
  onFiltersChanged(filters: any) {
    this.filteredCars = this.availableCars.filter((car) => {
      return (
        (!filters.price.length ||
          filters.price.some((p: string) =>
            this.isPriceInRange(car.prize, p)
          )) &&
        (!filters.driveType.length ||
          filters.driveType.includes(car.driveType)) &&
        (!filters.transmission.length ||
          filters.transmission.includes(car.transmission)) &&
        (!filters.rate.length ||
          filters.rate.some((r: string) => this.isRateInRange(car.rate, r)))
      );
    });
  }

  private isPriceInRange(price: number, range: string): boolean {
    const [min, max] = range.split('-').map((v) => parseInt(v, 10));
    return !isNaN(max) ? price >= min && price <= max : price >= min;
  }

  private isRateInRange(rate: number, range: string): boolean {
    const minRate = parseInt(range.replace('+', ''), 10);
    return rate >= minRate;
  }
}
