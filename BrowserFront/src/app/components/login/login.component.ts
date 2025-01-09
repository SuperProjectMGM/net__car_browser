import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

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
  errorMessage: string = '';
  isLoading: boolean = false;

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.isLoading = true;
    this.authService.login(this.email, this.password).subscribe({
      next: (response) => {
        console.log('logujemy sie');
        localStorage.setItem('loggedIn', 'true'); //TODO: ajkoś ogarnąc to logawanie w kontescie klucza Ustaw flagę zalogowania
        this.router.navigate(['/dashboard']);
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Login failed', err);
        this.errorMessage = 'Login failed. Please try again.';
        this.isLoading = false;
      },
    });
  }

    loginWithGoogle() {
      this.authService.googleLogin()
    }

  onSignUpClick() {
    this.isLoading = true;
    this.router.navigate(['/register']);
    this.isLoading = false;
  }
}
