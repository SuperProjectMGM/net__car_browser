import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isAuthenticated = false; // Domyślnie użytkownik jest niezalogowany

  // Symulacja logowania
  login(
    email: string,
    password: string
  ): Observable<{ success: boolean; message: string }> {
    console.log(`Logowanie: email=${email}, password=${password}`);
    this.isAuthenticated = true;
    return of({ success: true, message: 'Login successful' });
  }

  logout() {
    this.isAuthenticated = false; // Wylogowanie
    console.log('User logged out.');
  }

  isLoggedIn(): boolean {
    return this.isAuthenticated; // Sprawdzenie stanu zalogowania
  }
}
