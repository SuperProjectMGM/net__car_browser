import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { environment } from '../../environments/environment';
import { VehicleDetail } from '../models/VehicleDetail.model';

@Injectable({
  providedIn: 'root',
})
export class CarsService {
  private apiUrl = environment.apiBaseUrl;
  private availableCarsSubject = new BehaviorSubject<VehicleDetail[]>([]);
  private filteredCarsSubject = new BehaviorSubject<VehicleDetail[]>([]);

  constructor(private http: HttpClient) {}

  searchCars(
    rentalFrom: Date,
    rentalTo: Date,
    location: string
  ): Observable<VehicleDetail[]> {
    if (!rentalFrom || !rentalTo) {
      throw new Error(
        'RentalFrom and RentalTo dates must be provided and valid'
      );
    }

    const params = {
      rentalFrom: rentalFrom.toISOString(),
      rentalTo: rentalTo.toISOString(),
      location: location,
    };

    return this.http.get<VehicleDetail[]>(`${this.apiUrl}/Search`, { params });
  }

  setAvailableCars(cars: VehicleDetail[]): void {
    this.availableCarsSubject.next(cars);
  }

  setFilteredCars(cars: VehicleDetail[]): void {
    this.filteredCarsSubject.next(cars);
  }

  getAvailableCars(): Observable<VehicleDetail[]> {
    return this.availableCarsSubject.asObservable();
  }

  getFilteredCars(): Observable<VehicleDetail[]> {
    return this.filteredCarsSubject.asObservable();
  }

  clearAvailableCars(): void {
    this.availableCarsSubject.next([]);
  }

  clearFilteredCars(): void {
    this.filteredCarsSubject.next([]);
  }
}
