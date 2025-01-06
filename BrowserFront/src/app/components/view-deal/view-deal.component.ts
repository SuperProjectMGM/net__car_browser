import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { VehicleDetail } from '../../models/VehicleDetail.model';
import { CommonModule } from '@angular/common';
import { VehicleToRentService } from '../../services/VehicleToRent.service';
import { AuthService } from '../../services/auth.service';
import { ProfileService } from '../../services/profile.service';
import { ToastrService } from 'ngx-toastr';

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
  isModalVisible: boolean = false;

  constructor(
    private authService: AuthService,
    private vehicleToRentService: VehicleToRentService,
    private router: Router,
    private profileService: ProfileService,
    private toastr: ToastrService // Dodano ToastrService
  ) {}

  ngOnInit(): void {
    this.car = this.vehicleToRentService.getCar();
    [this.pickupDateTime, this.returnDateTime] =
      this.vehicleToRentService.getDate();
    if (!this.car || !this.pickupDateTime || !this.returnDateTime) {
      console.error('No car data found');
      this.toastr.error('No vehicle data available.', 'Error');
      this.router.navigateByUrl('/cars-list');
    } else {
      this.calculateTotalPrice();
    }
  }

  calculateTotalPrice(): void {
    console.log(this.returnDateTime);
    if (this.pickupDateTime && this.returnDateTime && this.car) {
      const timeDiff = Math.abs(
        new Date(this.returnDateTime).getTime() -
          new Date(this.pickupDateTime).getTime()
      );
      const days = Math.ceil(timeDiff / (1000 * 60 * 60 * 24));
      this.totalPrice = days * this.car.price;
    }
  }

  goToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }

  closeModal(): void {
    this.isModalVisible = false;
  }

  rentCar(): void {
    if (!this.authService.isLoggedIn()) {
      this.toastr.warning(
        'You must log in to rent a vehicle.',
        'You are not logged in'
      );
      this.router.navigate(['/login']);
      return;
    }

    this.profileService.isUserProfileComplete().subscribe((isComplete) => {
      if (isComplete) {
        this.isModalVisible = true; // Pokaż modal
      } else {
        this.toastr.info(
          'Complete your profile details to rent a vehicle.',
          'Incomplete profile'
        );
        this.router.navigate(['/edit-profile']);
      }
    });
  }

  confirmRentCar(): void {
    this.isModalVisible = false; // Zamknij modal
    if (this.car) {
      console.log(this.car.vin);
      this.authService
        .wantRentVehicle(this.car, this.pickupDateTime, this.returnDateTime)
        .subscribe(
          (response) => {
            this.toastr.success(
              'Please check your email to confirm the reservation.',
              'We have received your request.'
            );
            console.log('Rent request successful', response);
          },
          (error) => {
            this.toastr.error(
              'An error occurred while booking the vehicle.',
              'Error'
            );
            console.error('Error during vehicle rent request', error);
          }
        );
    }
  }
}
