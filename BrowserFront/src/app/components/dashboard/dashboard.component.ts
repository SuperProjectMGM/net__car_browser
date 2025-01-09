import { Component } from '@angular/core';
import { CarsListComponent } from '../cars-list/cars-list.component';
import { CommonModule, Time } from '@angular/common';
import { Router } from '@angular/router';
import { FiltersComponent } from '../filters/filters.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../../services/auth.service';
import { VehicleDetail } from '../../models/VehicleDetail.model';
import { CarsService } from '../../services/cars.service';
import { VehicleToRentService } from '../../services/VehicleToRent.service';

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
  isLoading: boolean = false;
  pickupDateTimeToreserve: Date | null = null;
  returnDateTimeToreserve: Date | null = null;
  constructor(
    private carsService: CarsService,
    private router: Router,
    private authService: AuthService,
    public vehicleToRentService: VehicleToRentService
  ) {}

  onSearch() {
    this.carsService.clearAvailableCars();
    this.carsService.clearFilteredCars();
    if (
      this.vehicleToRentService.pickupDateTime &&
      this.vehicleToRentService.returnDateTime &&
      this.vehicleToRentService.pickupLocation
    ) {
      const pickupDate = new Date(this.vehicleToRentService.pickupDateTime); // Konwersja string -> Date
      const returnDate = new Date(this.vehicleToRentService.returnDateTime);
      this.vehicleToRentService.setDate(
        this.vehicleToRentService.pickupDateTime,
        this.vehicleToRentService.returnDateTime
      );

      if (isNaN(pickupDate.getTime()) || isNaN(returnDate.getTime())) {
        console.error('Invalid date format');
        return;
      }

      this.isLoading = true;

      this.carsService
        .searchCars(
          pickupDate,
          returnDate,
          this.vehicleToRentService.pickupLocation
        )
        .subscribe(
          (cars) => {
            this.isLoading = false;
            this.carsService.setAvailableCars(cars);
            this.carsService.setFilteredCars(cars);
          },
          (error) => {
            this.isLoading = false;
            console.error('Wystąpił błąd przy wyszukiwaniu samochodów:', error);
          }
        );
    } else {
      console.error('Both pickup and return dates must be set.');
    }
  }

  goToProfile() {
    if (this.authService.isAuthenticated()) {
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
  onFiltersChanged(filters: any): void {
    this.carsService.getAvailableCars().subscribe((availableCars) => {
      if (!availableCars || availableCars.length === 0) {
        console.warn('No available cars to filter.');
        this.carsService.setFilteredCars([]);
        return;
      }

      const filteredCars = availableCars.filter((car) => {
        return (
          (!filters.price ||
            !filters.price.length ||
            filters.price.some((p: string) =>
              this.isPriceInRange(car.price, p)
            )) &&
          (!filters.driveType ||
            !filters.driveType.length ||
            filters.driveType.includes(car.driveType)) &&
          (!filters.transmission ||
            !filters.transmission.length ||
            filters.transmission.includes(car.transmission)) &&
          (!filters.rate ||
            !filters.rate.length ||
            filters.rate.some((r: string) =>
              this.isRateInRange(car.rate, r)
            )) &&
          (!filters.brand ||
            !filters.brand.length ||
            filters.brand.includes(car.brand)) &&
          (!filters.model ||
            !filters.model.length ||
            filters.model.includes(car.model))
        );
      });

      this.carsService.setFilteredCars(filteredCars);
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

  get isLoggedIn(): boolean {
    console.log("tu jestem", this.authService.isAuthenticated());
    return this.authService.isAuthenticated();
  }
}
