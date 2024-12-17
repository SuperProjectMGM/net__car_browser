import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { VehicleDetail } from '../../models/VehicleDetail.model';
import { CarService } from '../../services/car.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-view-deal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './view-deal.component.html',
  styleUrls: ['./view-deal.component.css'],
})
export class ViewDealComponent {
  car: VehicleDetail | null = null;

  constructor(private carService: CarService, private router: Router) {}

  ngOnInit(): void {
    this.car = this.carService.getCar(); // Pobierz dane z serwisu
    if (!this.car) {
      console.error('No car data found');
      // Opcjonalnie: przekierowanie w razie braku danych
      this.router.navigateByUrl('/cars-list');
    }
  }
}
