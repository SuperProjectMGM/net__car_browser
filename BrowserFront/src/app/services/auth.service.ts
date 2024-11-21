import { Injectable } from '@angular/core';
<<<<<<< HEAD
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
=======
import { Observable, of } from 'rxjs';
>>>>>>> 5fc086e23be154686c308ce06bac4d41af9d4d24

@Injectable({
  providedIn: 'root',
})
export class AuthService {
<<<<<<< HEAD
  private apiUrl = environment.apiBaseUrl;

  //   constructor(private http: HttpClient) {}

  //   login(email: string, password: string): Observable<any> {
  //     return this.http
  //       .post<any>(
  //         `${this.apiUrl}/Auth/login`,
  //         { email, password },
  //         { withCredentials: true }
  //       )
  //       .pipe(
  //         tap((response) => {
  //           if (response && response.token) {
  //             localStorage.setItem('loggedIn', 'true'); // Ustawienie flagi logowania
  //             console.log('ustawiono falge');
  //           }
  //         })
  //       );
  //   }

  //   logout(): Observable<any> {
  //     return this.http
  //       .post(`${this.apiUrl}/logout`, {}, { withCredentials: true })
  //       .pipe(
  //         tap(() => {
  //           localStorage.removeItem('loggedIn'); // Usunięcie flagi logowania
  //         })
  //       );
  //   }

  //   isAuthenticated(): boolean {
  //     // Tu możesz w przyszłości dodać logikę sprawdzającą sesję
  //     if (localStorage.getItem('loggedIn') == 'true') {
  //       return true;
  //     }
  //     return false;
  //   }
=======
  private isAuthenticated = false;

  // TODO: zrobić tutaj obsługe logowania
  login(
    email: string,
    password: string
  ): Observable<{ success: boolean; message: string }> {
    console.log(`Logowanie: email=${email}, password=${password}`);
    this.isAuthenticated = true;
    return of({ success: true, message: 'Login successful' });
  }

  logout() {
    this.isAuthenticated = false;
    console.log('User logged out.');
  }

  isLoggedIn(): boolean {
    return this.isAuthenticated;
  }
>>>>>>> 5fc086e23be154686c308ce06bac4d41af9d4d24
}
