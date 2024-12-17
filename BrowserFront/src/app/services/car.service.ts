import { Injectable } from '@angular/core';
import { VehicleDetail } from '../models/VehicleDetail.model';

@Injectable({
  providedIn: 'root',
})
export class CarService {
  private selectedCar: VehicleDetail | null = null;

  setCar(car: VehicleDetail): void {
    this.selectedCar = car;
  }

  getCar(): VehicleDetail | null {
    return this.selectedCar;
  }
}
