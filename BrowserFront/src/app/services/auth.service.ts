import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { Route, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { VehicleDetail } from '../models/VehicleDetail.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private apiUrl = environment.apiBaseUrl;
  private _isAuthenticated: boolean = false;
  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    const loginModel = { email, password };

    return this.http
      .post<{ token: string; expiration: string }>(
        `${this.apiUrl}/Authenticate/login`,
        loginModel,
        { withCredentials: true }
      )
      .pipe(
        tap((response) => {
          if (response && response.token) {
            localStorage.setItem('loggedIn', 'true');
            localStorage.setItem('token', response.token);
            console.log('Zalogowano pomyślnie, ustawiono flagę i zapisano token.');
            this._isAuthenticated = true;
          }
        })
      );
     }

     isAuthenticated(): boolean {
       // Tu możesz w przyszłości dodać logikę sprawdzającą sesję
       if (localStorage.getItem('loggedIn') == 'true') {
         return true;
       }
       return false;
     }

  
  wantRentVehicle(car: VehicleDetail, pickupDateTime: Date | null, returnDateTime: Date| null): Observable<any> {

    if(!pickupDateTime || !returnDateTime) return throwError(() => new Error('Pickup date and return date are required'));

    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    const vehicleRentRequest = {
      vehicleVin: car.vinId,        
      rentalFirmId: '1',  // TODO: zrobić wybieranie firmy          
      start: pickupDateTime,        
      end: returnDateTime,          
      description: car.description
    };
    return this.http.post<any>(
      `${this.apiUrl}/Rental/rent-car`, 
      vehicleRentRequest, 
      { headers }
    )
  }

  logout() {
    this._isAuthenticated = false;
    console.log('User logged out.');
  }

  isLoggedIn(): boolean {
    return this._isAuthenticated;
  }
}

export interface loginModel 
{
  email: string,
  password: string
}

export interface VehicleRentRequest {
  vehicleVin: string;
  rentalFirmId: string;
  start: Date;
  end: Date;
  description: string;
}

