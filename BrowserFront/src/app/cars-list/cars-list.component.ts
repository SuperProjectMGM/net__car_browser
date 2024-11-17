import { Component } from '@angular/core';
import { environment } from '../../enviroments/environment';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cars-list',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './cars-list.component.html',
  styleUrl: './cars-list.component.css',
})
export class CarsListComponent {
  cars: VehicleDetail[] = []; // To hold the list of cars
  ApiUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadCars(); // Call the method to load car data
  }

  loadCars() {
    this.getCars().subscribe((data) => {
      this.cars = data; // Assign fetched data to the cars array
    });
  }

  getCars() {
    return this.http.get<VehicleDetail[]>(`${this.ApiUrl}/Search`);
  }
}

export class VehicleDetail {
  carId: number = 0;
  brand: string = '';
  model: string = '';
  //serialNo: string = '';
  //vinId: string = '';
  //registryNo: string = '';
  yearOfProduction: string = ''; // Można również użyć `number` jeśli potrzebujesz tylko roku
  //rentalFrom: string = ''; // Zakładam, że data jest przechowywana w formacie string
  //rentalTo: string = ''; // Zakładam, że data jest przechowywana w formacie string
  //description: string = '';
  type: string = '';
}

