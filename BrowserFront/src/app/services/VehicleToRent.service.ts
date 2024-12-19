import { Injectable } from '@angular/core';
import { VehicleDetail } from '../models/VehicleDetail.model';

@Injectable({
  providedIn: 'root',
})
export class VehicleToRentService {
  private selectedCar: VehicleDetail | null = null;
  private pickupDateTime: Date | null = null;
  private returnDateTime: Date | null = null;

  setCar(car: VehicleDetail): void {
    this.selectedCar = car;
  }

  setDate(pickupDate: Date, returnDate: Date) {
    this.pickupDateTime = pickupDate;
    this.returnDateTime = returnDate;
  }

  getCar(): VehicleDetail | null {
    return this.selectedCar;
  }

  getDate(): [Date, Date] {
    if (this.pickupDateTime && this.returnDateTime) {
      return [this.pickupDateTime, this.returnDateTime];
    } else {
      return [new Date(), new Date()];
    }
  }
}
