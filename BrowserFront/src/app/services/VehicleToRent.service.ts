import { Injectable } from '@angular/core';
import { VehicleDetail } from '../models/VehicleDetail.model';

@Injectable({
  providedIn: 'root',
})
export class VehicleToRentService {
  private selectedCar: VehicleDetail | null = null;
  public pickupDateTime: Date | null = null;
  public returnDateTime: Date | null = null;
  public pickupLocation: string = '';

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
