import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = ''; // Zmienna na komunikaty błędów
  isLoading: boolean = false;

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.isLoading = true;

    // Tu trzeba zmodyfikowac by było zpaytanie do serwera o logowanie
    this.authService.login(this.email, this.password).subscribe({
      next: (response) => {
        localStorage.setItem('loggedIn', 'true'); // Ustaw flagę zalogowania
        this.router.navigate(['/dashboard']); // Przekierowanie do chronionej strony
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Login failed', err);
        this.errorMessage = 'Login failed. Please try again.';
        this.isLoading = false;
      },
    });
  }
  onSignUpClick() {
    this.isLoading = true;
    this.router.navigate(['/register']);
    this.isLoading = false;
  }
}
