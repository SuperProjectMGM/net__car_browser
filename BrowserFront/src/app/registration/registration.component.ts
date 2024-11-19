import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  standalone: true,
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css',
})
export class RegistrationComponent {
  constructor(/*private authService: AuthService*/ private router: Router) {}

  onSubmit() {
    console.log('Form submitted');
    // Tutaj możesz dodać kod obsługujący dane formularza,
    // np. wysłanie danych do serwera
  }

  gotoLogin() {
    this.router.navigate(['/login']);
  }
}
