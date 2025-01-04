import { Component, Input, SimpleChanges } from '@angular/core';
import { environment } from '../../../environments/environment';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ProfileService } from '../../services/profile.service';
import { VehicleDetail } from '../../models/VehicleDetail.model';
import { VehicleRentRequest } from '../../models/VehicleRentRequest.model';
import { VehicleToRentService } from '../../services/VehicleToRent.service';

@Component({
  selector: 'app-cars-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cars-list.component.html',
  styleUrls: ['./cars-list.component.css'],
})
export class CarsListComponent {
  ApiUrl = environment.apiBaseUrl;
  @Input() cars: VehicleDetail[] = [];
  @Input() pickupDateTime: Date | null = null;
  @Input() returnDateTime: Date | null = null;
  modalVisible = false;

  constructor(
    private profileSerive: ProfileService,
    private authService: AuthService,
    private router: Router,
    private vehicleToRentService: VehicleToRentService // Dodaj CarService
  ) {}

  ngOnChanges(changes: SimpleChanges) {
    if (changes['cars']) {
      console.log('Nowe dane samochodów:', this.cars);
    }
  }

  viewDeal(car: VehicleDetail): void {
    this.vehicleToRentService.setCar(car);
    if (this.pickupDateTime && this.returnDateTime) {
      this.vehicleToRentService.setDate(
        this.pickupDateTime,
        this.returnDateTime
      );
    } else {
      console.log('nie ma daty');
    }
    this.router.navigate(['/view-deals']);
  }
}
