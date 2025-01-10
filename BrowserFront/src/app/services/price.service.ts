import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class PriceService {
  private apiUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  getPriceForCar(
    price: number,
    token: string,
    start: Date,
    end: Date
  ): Observable<number> {
    if (!start || !end || !token || !price) {
      throw new Error(
        'RentalFrom and RentalTo dates must be provided and valid'
      );
    }

    const params = {
      price: price,
      token: token,
      start: start.toString(),
      end: end.toString(),
    };

    return this.http.get<number>(`${this.apiUrl}/Search/price`, { params });
  }
}
