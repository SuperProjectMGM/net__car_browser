import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProfileService } from '../services/profile.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css'],
})
export class EditProfileComponent implements OnInit {
  user: any = null;
  isLoading: boolean = true;

  constructor(private profileService: ProfileService, private router: Router) {}

  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    this.profileService.getUserProfile().subscribe((profile) => {
      this.user = profile;
      this.isLoading = false;
    });
  }

  onSubmit(): void {
    this.profileService.updateUserProfile(this.user).subscribe(
      () => {
        alert('Profile updated successfully!');
        this.router.navigate(['/profile']);
      },
      (error) => {
        console.error('Failed to update profile:', error);
        alert('Failed to update profile. Please try again.');
      }
    );
  }

  cancel(): void {
    this.router.navigate(['/profile']);
  }
}
