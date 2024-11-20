import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProfileService } from '../services/profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  user: UserProfile | null = null;
  isLoading: boolean = true;
  error: string | null = null;

  constructor(private profileService: ProfileService, private router: Router) {}

  ngOnInit(): void {
    this.loadUserProfile();
  }

  // Ładowanie danych użytkownika
  loadUserProfile() {
    this.profileService.getUserProfile().subscribe(
      (profile: UserProfile) => {
        this.user = profile;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error loading profile:', error);
        this.error = 'Failed to load user profile. Please try again.';
        this.isLoading = false;
      }
    );
  }

  // Przekierowanie do edycji profilu
  editProfile() {
    this.router.navigate(['/edit-profile']);
  }

  // Wylogowanie użytkownika
  logout() {
    this.profileService.logout();
    this.router.navigate(['/login']);
  }
}

export interface UserProfile {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  address: {
    street: string;
    postalCode: string;
    city: string;
  };
  dateOfBirth: string;
  drivingLicense: {
    number: string;
    issueDate: string;
    expirationDate: string;
  };
  idCard: {
    number: string;
    issuedBy: string;
    issueDate: string;
    expirationDate: string;
  };
}
