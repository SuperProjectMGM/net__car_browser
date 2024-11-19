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

  constructor(private carsService: CarsService, private router: Router) {
    console.log('HttpClient is ready');
  }

  onSearch() {
    this.availableCars = [];
    if (this.pickupDateTime && this.returnDateTime) {
      const pickupDate = new Date(this.pickupDateTime); // Konwersja string -> Date
      const returnDate = new Date(this.returnDateTime);

      if (isNaN(pickupDate.getTime()) || isNaN(returnDate.getTime())) {
        console.error('Invalid date format');
        return;
      }

      this.isLoading = true;

      this.carsService.searchCars(pickupDate, returnDate).subscribe(
        (cars) => {
          this.isLoading = false;
          this.availableCars = cars; // Przechowujemy dostępne samochody
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
    this.isLoading = true;
    this.router.navigate(['/profile']);
    this.isLoading = false;
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
}
