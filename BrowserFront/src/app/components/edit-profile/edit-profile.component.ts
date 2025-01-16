import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ProfileService } from '../../services/profile.service';
import { UserProfile } from '../../models/UserProfile.model';
import { CommonModule } from '@angular/common';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css'],
})
export class EditProfileComponent implements OnInit {
  user: UserProfile | null = null;
  isLoading: boolean = true;
  today: string = new Date().toISOString().split('T')[0];
  eighteenYearsAgo: string = '';

  constructor(private profileService: ProfileService, private router: Router) {}

  ngOnInit(): void {
    this.loadUserProfile();
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

  loadUserProfile(): void {
    this.profileService.getUserProfile().subscribe((profile) => {
      if (profile) {
        profile.birthDate = this.formatDate(profile.birthDate);
        profile.drivingLicenseIssueDate = this.formatDate(
          profile.drivingLicenseIssueDate
        );
        profile.drivingLicenseExpirationDate = this.formatDate(
          profile.drivingLicenseExpirationDate
        );
        profile.idCardIssueDate = this.formatDate(profile.idCardIssueDate);
        profile.idCardExpirationDate = this.formatDate(
          profile.idCardExpirationDate
        );

        this.user = profile;
      }
      this.isLoading = false;
    });
  }

  formatDate(date: string | Date | null | undefined): string | undefined {
    if (!date) return undefined;
    const d = new Date(date);
    const month = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${d.getFullYear()}-${month}-${day}`;
  }

  onSubmit(): void {
    if (!this.user?.birthDate) {
      alert('Date of birth is required.');
      return;
    }

    const birthDate = this.user?.birthDate
      ? new Date(this.user.birthDate)
      : null;
    const eighteenYearsAgo = new Date(this.eighteenYearsAgo);

    if (birthDate && birthDate > eighteenYearsAgo) {
      alert('You must be at least 18 years old.');
      return;
    }

    if (this.user) {
      this.profileService.updateUserProfile(this.user).subscribe(
        () => {
          this.router.navigate(['/profile']);
        },
        (error) => {
          console.error('Failed to update profile:', error);
          alert('Failed to update profile. Please try again.');
        }
      );
    } else {
      console.log('Failed to load user data');
    }
  }

  cancel(): void {
    this.router.navigate(['/profile']);
  }

  validateDates(): void {
    if (this.user) {
      const birthDate = this.user.birthDate
        ? new Date(this.user.birthDate)
        : alert('enter date.');
      const eighteenYearsAgo = new Date(this.eighteenYearsAgo);
      if (birthDate > eighteenYearsAgo) {
        alert('Date of Birth must be at least 18 years in the past.');
        this.user.birthDate = '';
      }

      if (
        this.user.drivingLicenseIssueDate &&
        this.user.drivingLicenseExpirationDate
      ) {
        const issueDate = new Date(this.user.drivingLicenseIssueDate);
        const expirationDate = new Date(this.user.drivingLicenseExpirationDate);
        if (expirationDate <= issueDate) {
          alert(
            'Driving License Expiration Date must be after the Issue Date.'
          );
          this.user.drivingLicenseExpirationDate = '';
        }
      }

      if (this.user.idCardIssueDate && this.user.idCardExpirationDate) {
        const idIssueDate = new Date(this.user.idCardIssueDate);
        const idExpirationDate = new Date(this.user.idCardExpirationDate);
        if (idExpirationDate <= idIssueDate) {
          alert('ID Card Expiration Date must be after the Issue Date.');
          this.user.idCardExpirationDate = '';
        }
      }
    }
  }
}
