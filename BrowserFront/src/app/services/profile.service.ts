import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

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
        number: 'ID123456',
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

  isUserProfileComplete(): Observable<boolean> {
    return this.getUserProfile().pipe(
      catchError(() => {
        console.error('Error fetching user profile');
        // Zwracamy profil z pustymi polami jako fallback
        return of({
          firstName: '',
          lastName: '',
          email: '',
          phoneNumber: '',
          address: { street: '', postalCode: '', city: '' },
          dateOfBirth: '',
          drivingLicense: { number: '', issueDate: '', expirationDate: '' },
          idCard: {
            number: '',
            issuedBy: '',
            issueDate: '',
            expirationDate: '',
          },
        } as UserProfile);
      }),
      map((profile: UserProfile) => {
        // Tworzymy listę wymaganych pól
        const requiredFields = [
          profile.firstName,
          profile.lastName,
          profile.email,
          profile.phoneNumber,
          profile.address?.street,
          profile.address?.postalCode,
          profile.address?.city,
          profile.dateOfBirth,
          profile.drivingLicense?.number,
          profile.drivingLicense?.issueDate,
          profile.drivingLicense?.expirationDate,
          profile.idCard?.number,
          profile.idCard?.issuedBy,
          profile.idCard?.issueDate,
          profile.idCard?.expirationDate,
        ];

        // Sprawdzamy, czy któreś pole jest puste lub zawiera tylko białe znaki
        const isComplete = requiredFields.every((field) => {
          if (typeof field === 'string') {
            return field.trim() !== ''; // Sprawdzamy, czy tekst nie jest pusty ani nie zawiera tylko białych znaków
          }
          return field !== null && field !== undefined; // Sprawdzamy wartości inne niż tekst
        });

        return isComplete;
      })
    );
  }
}
