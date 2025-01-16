import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { VehicleDetail } from '../models/VehicleDetail.model';

@Injectable({
  providedIn: 'root',
})
export class PriceService {
  private apiUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  getPriceForCar(
    car: VehicleDetail,
    token: string,
    start: Date,
    end: Date
  ): Observable<number> {
    if (!start || !end || !token || !car) {
      throw new Error('RentalFrom and RentalTo dates must be provided and valid');
    }
  
    const params = new HttpParams()
      .set('token', token)
      .set('start', start.toString())
      .set('end', end.toString());
  
    return this.http.post<number>(
      `${this.apiUrl}/Search/price`, 
      car, 
      { params}
    );
  }
}
