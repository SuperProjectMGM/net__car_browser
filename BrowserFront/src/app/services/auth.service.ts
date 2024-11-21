import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Route, Router } from '@angular/router';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private apiUrl = environment.apiBaseUrl;
  private _isAuthenticated: boolean = false;
  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    const loginModel = { email, password }; // Tworzymy model zgodny z backendem

    return this.http
      .post<{ token: string; expiration: string }>(
        `${this.apiUrl}/Authenticate/login`, // Ścieżka do endpointu backendu
        loginModel, // Wysyłamy model w formacie JSON
        { withCredentials: true } // Opcjonalne, jeśli używasz ciasteczek do autoryzacji
      )
      .pipe(
        tap((response) => {
          if (response && response.token) {
            localStorage.setItem('loggedIn', 'true'); // Ustawienie flagi logowania
            localStorage.setItem('token', response.token); // Przechowywanie tokena w localStorage
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

  // TODO: zrobić tutaj obsługe logowania
  

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
