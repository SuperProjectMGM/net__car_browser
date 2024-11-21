import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ProfileService } from '../../services/profile.service';

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
