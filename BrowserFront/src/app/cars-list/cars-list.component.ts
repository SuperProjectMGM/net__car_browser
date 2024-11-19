import { Component, Input, SimpleChanges } from '@angular/core';
import { environment } from '../../enviroments/environment';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cars-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cars-list.component.html',
  styleUrl: './cars-list.component.css',
})
export class CarsListComponent {
  ApiUrl = environment.apiBaseUrl;
  @Input() cars: VehicleDetail[] = [];

  ngOnChanges(changes: SimpleChanges) {
    if (changes['cars']) {
      // Możesz tutaj wykonać jakieś operacje po otrzymaniu nowych danych
      console.log('Nowe dane samochodów:', this.cars);
    }
  }
}

export class VehicleDetail {
  carId: number = 0;
  brand: string = '';
  model: string = '';
  serialNo: string = '';
  vinId: string = '';
  yearOfProduction: number = 0;
  rentalFrom: Date = new Date();
  rentalTo: Date = new Date();
  prize: number = 0.0;
  driveType: string = '';
  transmission: string = '';
  description: string = '';
  type: string = '';
  rate: number = 0.0;
  localization: string = '';
}
