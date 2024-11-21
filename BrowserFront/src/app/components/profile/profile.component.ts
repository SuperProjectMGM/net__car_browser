import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProfileService } from '../../services/profile.service';
import { UserProfile } from '../../models/UserProfile.model';

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

  editProfile() {
    this.router.navigate(['/edit-profile']);
  }

  logout() {
    this.profileService.logout();
    this.router.navigate(['/login']);
  }

  GotoDashborad() {
    this.router.navigate(['/dashborad']);
  }
}
