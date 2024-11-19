import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../enviroments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
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
}
