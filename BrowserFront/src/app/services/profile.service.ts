import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

interface UserProfile {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  address: {
    street: string;
    postalCode: string;
    city: string;
  };
  dateOfBirth: string;
  drivingLicense: {
    number: string;
    issueDate: string;
    expirationDate: string;
  };
  idCard: {
    number: string;
    issuedBy: string;
    issueDate: string;
    expirationDate: string;
  };
}

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private apiUrl = 'https://api.example.com/user'; // TODO: ustawienie url po siegniecie po dane uzytkownika

  constructor(private http: HttpClient) {}

  // Pobieranie danych użytkownika
  getUserProfile(): Observable<UserProfile> {
    // return this.http.get<UserProfile>(`${this.apiUrl}/profile`).pipe(
    //   catchError((error) => {
    //     console.error('Error fetching user profile:', error);
    //     // Zwracamy pusty profil jako fallback
    //     return of({
    //       firstName: '',
    //       lastName: '',
    //       email: '',
    //       phoneNumber: '',
    //       address: { street: '', postalCode: '', city: '' },
    //       dateOfBirth: '',
    //       drivingLicense: { number: '', issueDate: '', expirationDate: '' },
    //       idCard: { number: '', issuedBy: '', issueDate: '', expirationDate: '' },
    //     } as UserProfile);
    //   })
    // );

    const mockUserProfile: UserProfile = {
      firstName: 'John',
      lastName: 'Doe',
      email: 'john.doe@example.com',
      phoneNumber: '+1234567890',
      address: {
        street: '123 Main St',
        postalCode: '00-123',
        city: 'Example City',
      },
      dateOfBirth: '1990-01-01',
      drivingLicense: {
        number: 'DL123456',
        issueDate: '2015-06-15',
        expirationDate: '2025-06-15',
      },
      idCard: {
        number: 'ID987654',
        issuedBy: 'Government Office',
        issueDate: '2020-01-01',
        expirationDate: '2030-01-01',
      },
    };

    return of(mockUserProfile);
  }

  updateUserProfile(user: any): Observable<any> {
    console.log('Simulating profile update:', user);
    // TODO: wysłac put na api zeby zedytowac dane o uzytkowniku
    return of({ success: true, message: 'Profile updated successfully!' });
  }

  // Wylogowanie użytkownika
  logout(): void {
    localStorage.removeItem('authToken');
    console.log('User has been logged out.');
  }
}
