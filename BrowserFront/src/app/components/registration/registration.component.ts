import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { RegisterModel } from '../../models/RegisterModel.model';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
})
export class RegistrationComponent {
  errorMessage: string = '';
  eighteenYearsAgo: string = '';
  registerModel: RegisterModel = {
    email: '',
    password: '',
    name: '',
    surname: '',
    userName: '',
    phoneNumber: '',
    streetAndNumber: '',
    postalCode: '',
    city: '',
    birthDate: '',
    drivingLicenseNumber: '',
    drivingLicenseIssueDate: '',
    drivingLicenseExpirationDate: '',
    personalNumber: '',
    idCardIssuedBy: '',
    idCardIssueDate: '',
    idCardExpirationDate: '',
  };

  constructor(private authService: AuthService, private router: Router) {
    this.calculateEighteenYearsAgo();
  }

  calculateEighteenYearsAgo(): void {
    const today = new Date();
    const eighteenYearsAgo = new Date(
      today.getFullYear() - 18,
      today.getMonth(),
      today.getDate()
    );
    this.eighteenYearsAgo = eighteenYearsAgo.toISOString().split('T')[0];
  }

  onRegister(registerForm: NgForm): void {
    if (registerForm.invalid) {
      this.errorMessage = 'Please fill in all required fields.';
      return;
    }

    // Sprawdzenie znaku '@' w email
    if (!this.registerModel.email.includes('@')) {
      this.errorMessage = "Invalid email: must contain '@'.";
      return;
    }

    let userBirthDate: Date;
    if (this.registerModel.birthDate) {
      userBirthDate = new Date(this.registerModel.birthDate);
    } else {
      alert('Please enter Date of Birth.');
      return;
    }

    const cutoffDate = new Date(this.eighteenYearsAgo);
    if (userBirthDate > cutoffDate) {
      this.errorMessage = 'You must be at least 18 years old to register.';
      return;
    }

    let dlIssueDate: Date;
    if (this.registerModel.drivingLicenseIssueDate) {
      dlIssueDate = new Date(this.registerModel.drivingLicenseIssueDate);
    } else {
      alert('Please enter Driving License Issue Date.');
      return;
    }

    let dlExpirationDate: Date;
    if (this.registerModel.drivingLicenseExpirationDate) {
      dlExpirationDate = new Date(
        this.registerModel.drivingLicenseExpirationDate
      );
    } else {
      alert('Please enter Driving License Expiration Date.');
      return;
    }

    if (dlIssueDate > dlExpirationDate) {
      this.errorMessage =
        'Driving License Expiration Date must be after the Issue Date.';
      return;
    }

    let idIssueDate: Date;
    if (this.registerModel.idCardIssueDate) {
      idIssueDate = new Date(this.registerModel.idCardIssueDate);
    } else {
      alert('Please enter ID Card Issue Date.');
      return;
    }

    let idExpirationDate: Date;
    if (this.registerModel.idCardExpirationDate) {
      idExpirationDate = new Date(this.registerModel.idCardExpirationDate);
    } else {
      alert('Please enter ID Card Expiration Date.');
      return;
    }

    if (idIssueDate > idExpirationDate) {
      this.errorMessage =
        'ID Card Expiration Date must be after the Issue Date.';
      return;
    }

    console.log('Form submitted', this.registerModel);
    this.authService.takeRegister(this.registerModel).subscribe(
      (response) => {
        console.log('Registration successful', response);
        this.router.navigate(['/login']);
      },
      (error) => {
        console.error('Registration failed', error);
        this.errorMessage = 'Registration failed. Please try again.';
      }
    );
  }

  onCancel(): void {
    this.router.navigate(['/login']);
  }
}
