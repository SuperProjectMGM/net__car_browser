import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
<<<<<<< HEAD
import { VehicleDetail } from '../cars-list/cars-list.component';
=======
import { VehicleDetail } from '../models/VehicleDetail.model';
>>>>>>> 5fc086e23be154686c308ce06bac4d41af9d4d24

@Injectable({
  providedIn: 'root',
})
export class CarsService {
  private apiUrl = environment.apiBaseUrl;

<<<<<<< HEAD
  constructor(private http: HttpClient) {
    console.log('HttpClient is ready');
  }
=======
  constructor(private http: HttpClient) {}
>>>>>>> 5fc086e23be154686c308ce06bac4d41af9d4d24

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

<<<<<<< HEAD
    // Konwersja dat na ISO stringi
=======
>>>>>>> 5fc086e23be154686c308ce06bac4d41af9d4d24
    const params = {
      rentalFrom: rentalFrom.toISOString(),
      rentalTo: rentalTo.toISOString(),
      location: location,
    };

<<<<<<< HEAD
    // Wysyłanie zapytania do API
=======
>>>>>>> 5fc086e23be154686c308ce06bac4d41af9d4d24
    return this.http.get<VehicleDetail[]>(`${this.apiUrl}/Search`, { params });
  }
}
