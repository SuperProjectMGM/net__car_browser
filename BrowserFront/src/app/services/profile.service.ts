import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { UserProfile } from '../models/UserProfile.model';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private apiUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient, private authService: AuthService) {}

  getUserProfile(): Observable<UserProfile> {
    const token = this.authService.getToken();
    if (token === null) {
      return throwError(() => new Error('Error with jwt token'));
    }

    const params = new HttpParams().set('token', token);
    return this.http
      .get<UserProfile>(`${this.apiUrl}/UserInfo/user-info`, { params })
      .pipe(
        catchError((error) => {
          console.error('Error fetching user profile:', error);
          return of({
            email: '',
            phoneNumber: '',
            name: '',
            surname: '',
            birthDate: '',
            drivingLicenseNumber: '',
            drivingLicenseIssueDate: '',
            drivingLicenseExpirationDate: '',
            postalCode: '',
            city: '',
            personalNumber: '',
            idCardIssuedBy: '',
            idCardIssueDate: '',
            idCardExpirationDate: '',
          } as UserProfile);
        })
      );
  }

  updateUserProfile(user: UserProfile): Observable<any> {
    const token = this.authService.getToken()
    if (token === null) {
      return throwError(() => new Error('Error with JWT token'));
    }

    const params = new HttpParams().set('token', token);

    return this.http
      .put(
        `${this.apiUrl}/UserInfo/change-user-info`,
        user,
        { params, responseType: 'text' as 'json' }
      )
      .pipe(
        catchError((error) => {
          console.error('Error updating user profile:', error);
          return throwError(() => new Error('Error updating user profile'));
        })
      );
  }

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
          streetAndNumber: '',
          postalCode: '',
          city: '',
          personalNumber: '',
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
          profile.streetAndNumber,
          profile.postalCode,
          profile.city,
          profile.birthDate,
          profile.drivingLicenseNumber,
          profile.drivingLicenseIssueDate,
          profile.drivingLicenseExpirationDate,
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
