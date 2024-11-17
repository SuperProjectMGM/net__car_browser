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
  cars: VehicleDetail[] = [];
  ApiUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadCars();
  }

  loadCars() {
    this.getCars().subscribe((data) => {
      this.cars = data;
    });
  }

  getCars() {
    return this.http.get<VehicleDetail[]>(`${this.ApiUrl}/Search`);
  }
}

// zwiekszyć ilość pobieranych danych

export class VehicleDetail {
  carId: number = 0;
  brand: string = '';
  model: string = '';
  //serialNo: string = '';
  //vinId: string = '';
  //registryNo: string = '';
  yearOfProduction: string = '';
  //rentalFrom: string = ''; // Zakładam, że data jest przechowywana w formacie string
  //rentalTo: string = ''; // Zakładam, że data jest przechowywana w formacie string
  //description: string = '';
  type: string = '';
}
