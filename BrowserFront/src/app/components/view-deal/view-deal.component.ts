import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { VehicleDetail } from '../../models/VehicleDetail.model';
import { CommonModule } from '@angular/common';
import { VehicleToRentService } from '../../services/VehicleToRent.service';

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

  constructor(
    private vehicleToRentService: VehicleToRentService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.car = this.vehicleToRentService.getCar();
    [this.pickupDateTime, this.returnDateTime] =
      this.vehicleToRentService.getDate();
    if (!this.car || !this.pickupDateTime || !this.returnDateTime) {
      console.error('No car data found');
      // Opcjonalnie: przekierowanie w razie braku danych
      this.router.navigateByUrl('/cars-list');
    }
  }
}
