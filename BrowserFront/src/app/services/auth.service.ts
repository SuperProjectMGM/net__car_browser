import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { VehicleDetail } from '../models/VehicleDetail.model';
import { RegisterModel } from '../models/RegisterModel.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = environment.apiBaseUrl;
  private _isAuthenticated: boolean = false;

  constructor(private http: HttpClient, private router: Router) {}

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
            sessionStorage.setItem('token', response.token);
            this._isAuthenticated = true;
            console.log('Login successful. Token stored.');
          }
        })
      );
  }

  isAuthenticated(): boolean {
    const token = sessionStorage.getItem('token');
    return !!token;
  }

  getToken(): string | null {
    return sessionStorage.getItem('token');
  }
  googleLogin() {
    window.location.href = `${this.apiUrl}/Authenticate/login/google-login`;
  }

  handleGoogleCallback(token: string) {
    sessionStorage.setItem('token', token);
    this._isAuthenticated = true;
    this.router.navigate(['/dashboard']);
    console.log('Google login successful.');
  }

  wantRentVehicle(
    car: VehicleDetail,
    pickupDateTime: Date | null,
    returnDateTime: Date | null
  ): Observable<any> {
    if (!pickupDateTime || !returnDateTime)
      return throwError(
        () => new Error('Pickup date and return date are required')
      );

    const token = this.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    const vehicleRentRequest = {
      vehicleVin: car.vin,
      carProviderIdentifier: car.rentalFirmName,
      start: pickupDateTime,
      end: returnDateTime,
      description: car.description,
    };
    return this.http.post<any>(
      `${this.apiUrl}/Rental/rent-car`,
      vehicleRentRequest,
      { headers }
    );
  }

  logout() {
    this._isAuthenticated = false;
    console.log('User logged out.');
    sessionStorage.clear();
    this.router.navigate(['/login']);
    console.log('User logged out.');
  }

  takeRegister(registerModel: RegisterModel): Observable<any> {
    return this.http
      .post<RegisterModel>(
        `${this.apiUrl}/Authenticate/register`,
        registerModel
      )
      .pipe(
        tap((response) => {
          if (response) {
            alert('Pomyślnie zarejestrowane!');
          }
        })
      );
  }
}
