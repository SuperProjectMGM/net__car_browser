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
  @Input() pickupDateTime: Date | null = null;
  @Input() returnDateTime: Date | null = null;

  ngOnChanges(changes: SimpleChanges) {
    if (changes['cars']) {
      console.log('Nowe dane samochodów:', this.cars);
    }
  }

  // tu trzeba zrobić wysyłanie na backend z chęcią wynajęcia samochodu
  rentCar(car: VehicleDetail) {
    console.log(
      'chęć wynajecia fury:',
      car.model,
      'w okresie:',
      this.pickupDateTime,
      ' - ',
      this.returnDateTime
    );
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
