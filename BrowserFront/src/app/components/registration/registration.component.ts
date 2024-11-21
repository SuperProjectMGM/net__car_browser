import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RegisterModel } from '../../models/RegisterModel.model';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports:[CommonModule, FormsModule],
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
})
export class RegistrationComponent {
  // Create an instance of RegisterModel to bind to the form
  registerModel: RegisterModel = {
    email: '',
    password: '',
    firstName: '',
    secondName: '',
    userName: '',
    phoneNumber: '',
    addressStreet: '',
    postalCode: '',
    city: '',
    dateOfBirth: '',
    drivingLicenseNumber: '',
    drivingLicenseIssueDate: '',
    drivingLicenseExpirationDate: '',
    idCardNumber: '',
    idCardIssuedBy: '',
    idCardIssueDate: '',
    idCardExpirationDate: ''
  };

  constructor(private authService: AuthService, private router: Router) {}

  onRegister() {
    console.log('Form submitted', this.registerModel);
    this.authService.takeRegister(this.registerModel).subscribe(
      response => {
        console.log('Registration successful', response);
        // Optionally navigate to another page (e.g., login page)
        this.router.navigate(['/login']);
      },
      error => {
        console.error('Registration failed', error);
      }
    );
  }

  onCancel() {
    this.router.navigate(['/login']);
  }
}
