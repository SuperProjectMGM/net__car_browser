import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
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
}
