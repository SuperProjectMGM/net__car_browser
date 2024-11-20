import { Component, Input, SimpleChanges } from '@angular/core';
import { environment } from '../../environments/environment';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

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

  constructor(
    private authService: AuthService, // Obsługa autoryzacji
    private router: Router // Obsługa nawigacji
  ) {}

  ngOnChanges(changes: SimpleChanges) {
    if (changes['cars']) {
      console.log('Nowe dane samochodów:', this.cars);
    }
  }

  // tu trzeba zrobić wysyłanie na backend z chęcią wynajęcia samochodu
  rentCar(car: VehicleDetail) {
    if (this.authService.isLoggedIn()) {
      console.log(
        'Chęć wynajęcia fury:',
        car.model,
        'w okresie:',
        this.pickupDateTime,
        ' - ',
        this.returnDateTime
      );
      // TODO: Dodaj logikę wynajmu samochodu
    } else {
      console.warn(
        'Użytkownik nie jest zalogowany. Przekierowanie na stronę logowania.'
      );
      this.router.navigate(['/login']);
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
