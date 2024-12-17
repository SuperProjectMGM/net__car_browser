import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { VehicleDetail } from '../../models/VehicleDetail.model';
import { CommonModule } from '@angular/common';
import { VehicleToRentService } from '../../services/VehicleToRent.service';
import { AuthService } from '../../services/auth.service';
import { ProfileService } from '../../services/profile.service';

@Component({
  selector: 'app-view-deal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './view-deal.component.html',
  styleUrls: ['./view-deal.component.css'],
})
export class ViewDealComponent {
  car: VehicleDetail | null = null;
  pickupDateTime: Date | null = null;
  returnDateTime: Date | null = null;
  totalPrice: number = 0;

  constructor(
    private authService: AuthService,
    private vehicleToRentService: VehicleToRentService,
    private router: Router,
    private profileService: ProfileService
  ) {}

  ngOnInit(): void {
    this.car = this.vehicleToRentService.getCar();
    [this.pickupDateTime, this.returnDateTime] =
      this.vehicleToRentService.getDate();
    if (!this.car || !this.pickupDateTime || !this.returnDateTime) {
      console.error('No car data found');
      // Opcjonalnie: przekierowanie w razie braku danych
      this.router.navigateByUrl('/cars-list');
    } else {
      this.calculateTotalPrice();
    }
  }

  rentCar(car: VehicleDetail): void {
    if (this.authService.isLoggedIn()) {
      this.profileService.isUserProfileComplete().subscribe((isComplete) => {
        if (isComplete) {
          this.authService
            .wantRentVehicle(car, this.pickupDateTime, this.returnDateTime)
            .subscribe(
              (response) => {
                // Obsługuje udaną odpowiedź
                console.log('Rent request successful', response); // TODO: co na froncie po wysłanius
              },
              (error) => {
                // Obsługuje błąd
                console.error('Error during vehicle rent request', error); // TODO: co dalej?
              }
            );
        } else {
          console.log('Nie ma wypełnionych danych profilu.');
          this.router.navigate(['/edit-profile']);
        }
      });
    } else {
      console.log('Użytkownik nie jest zalogowany.');
      this.router.navigate(['/login']);
    }
  }

  calculateTotalPrice(): void {
    if (this.pickupDateTime && this.returnDateTime && this.car) {
      const timeDiff = Math.abs(
        new Date(this.returnDateTime).getTime() -
          new Date(this.pickupDateTime).getTime()
      );
      const days = Math.ceil(timeDiff / (1000 * 60 * 60 * 24));
      this.totalPrice = days * this.car.prize;
    }
  }

  goToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }
}
