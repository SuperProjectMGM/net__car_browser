import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ProfileService } from '../../services/profile.service';
import { UserProfile } from '../../models/UserProfile.model';
import { CommonModule } from '@angular/common';

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

  constructor(private profileService: ProfileService, private router: Router) {}

  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    this.profileService.getUserProfile().subscribe((profile) => {
      if (profile) {
        // Konwersja dat do formatu YYYY-MM-DD
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

        this.user = profile; // Przypisanie do modelu
      }
      this.isLoading = false;
    });
  }

  formatDate(date: string | Date | null | undefined): string | undefined {
    if (!date) return undefined; // Zwróć null zamiast undefined
    const d = new Date(date);
    const month = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${d.getFullYear()}-${month}-${day}`; // Format YYYY-MM-DD
  }

  onSubmit(): void {
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
      console.log('cos nie tak z userem');
    }
  }

  cancel(): void {
    this.router.navigate(['/profile']);
  }
}
