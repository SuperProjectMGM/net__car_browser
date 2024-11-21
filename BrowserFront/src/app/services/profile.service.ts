import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { UserProfile } from '../models/UserProfile.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private apiUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  // TODO: endpoint Pobieranie danych użytkownika

  getUserProfile(): Observable<UserProfile> {
    const token = localStorage.getItem('token');
    if (token === null)
    {
      return throwError(() => new Error('Error with jwt token'));
    }
    const params = new HttpParams().set('token', token);
    return this.http.get<UserProfile>(`${this.apiUrl}/UserInfo/user-info`, { params } ).pipe(
      catchError((error) => {
        console.error('Error fetching user profile:', error);
        return of({
          userName: '',
          email: '',
          phoneNumber: '',
          name: '',
          surname: '',
          birthDate: '',
          drivingLicenseNumber: '',
          drivingLicenseIssueDate: '',
          drivingLicenseExpirationDate: '',
          addressStreet: '',
          postalCode: '',
          city: '',
          idPersonalNumber: '',
          idCardIssuedBy: '',
          idCardIssueDate: '',
          idCardExpirationDate: '',
        } as UserProfile);
      })
    );
  }
  

  updateUserProfile(user: UserProfile): Observable<any> {
    const token = localStorage.getItem('token');
    if (token === null) {
      return throwError(() => new Error('Error with JWT token'));
    }
  
    // Dodanie tokena jako parametru zapytania
    const params = new HttpParams().set('token', token);
  
    // Przesyłanie `user` jako ciało zapytania (body)
    return this.http.put(
      `${this.apiUrl}/UserInfo/change-user-info`,
      user, // Obiekt do edycji przesyłany w body
      { params, responseType: 'text' as 'json' } // Token dodany jako parametr, oczekiwana odpowiedź tekstowa
    ).pipe(
      catchError((error) => {
        console.error('Error updating user profile:', error);
        return throwError(() => new Error('Error updating user profile'));
      })
    );
  }
  
  

  // Wylogowanie użytkownika
  logout(): void {
    localStorage.removeItem('authToken');
    console.log('User has been logged out.');
  }

  isUserProfileComplete(): Observable<boolean> {
    return this.getUserProfile().pipe(
      catchError(() => {
        console.error('Error fetching user profile');
        return of({
          userName: '',
          email: '',
          phoneNumber: '',
          name: '',
          surname: '',
          birthDate: '',
          drivingLicenseNumber: '',
          drivingLicenseIssueDate: '',
          drivingLicenseExpirationDate: '',
          addressStreet: '',
          postalCode: '',
          city: '',
          idPersonalNumber: '',
          idCardIssuedBy: '',
          idCardIssueDate: '',
          idCardExpirationDate: '',
        } as UserProfile);
      }),
      map((profile: UserProfile) => {
        const requiredFields = [
          profile.name,
          profile.surname,
          profile.email,
          profile.phoneNumber,
          profile.addressStreet,
          profile.postalCode,
          profile.city,
          profile.birthDate,
          profile.drivingLicenseNumber,
          profile.drivingLicenseIssueDate,
          profile.drivingLicenseExpirationDate,
          profile.idPersonalNumber,
          profile.idCardIssuedBy,
          profile.idCardIssueDate,
          profile.idCardExpirationDate,
        ];
  
        const isComplete = requiredFields.every((field) => {
          if (typeof field === 'string') {
            return field.trim() !== '';
          }
          return field !== null && field !== undefined;
        });
  
        return isComplete;
      })
    );
  }
}
