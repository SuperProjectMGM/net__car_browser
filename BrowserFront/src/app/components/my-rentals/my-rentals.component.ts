import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Rental } from '../../models/RentalModel.model';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ProfileService } from '../../services/profile.service';
import { isEmpty } from 'rxjs';

@Component({
  selector: 'app-my-rentals',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './my-rentals.component.html',
  styleUrls: ['./my-rentals.component.css'],
})
export class MyRentalsComponent implements OnInit {
  rentals: Rental[] = [];
  baseUrl: string = environment.apiBaseUrl;

  isReturnModalVisible = false;
  isCancelModalVisible = false;
  selectedRental!: Rental;

  returnData = {
    latitude: '',
    longtitude: '',
    feedback: '',
  };
  cancelReason = '';

  constructor(
    private router: Router,
    private client: HttpClient,
    private profile: ProfileService
  ) {}

  ngOnInit(): void {
    this.fetchRentals();
  }

  fetchRentals() {
    this.profile.getUserProfile().subscribe({
      next: (userProfile) => {
        if (!userProfile.personalNumber) {
          console.error('Personal number is missing from user profile!');
          return;
        }

        const params = new HttpParams().set(
          'personalNumber',
          userProfile.personalNumber
        );
        this.client
          .get<Rental[]>(`${this.baseUrl}/Rental/get-my-rentals`, { params })
          .subscribe({
            next: (rentals) => {
              console.log('Fetched rentals:', rentals);
              this.rentals = rentals;
              console.log(rentals[0].status, typeof rentals[0].status);
            },
            error: (err) => {
              console.error('Error fetching rentals:', err);
            },
          });
      },
      error: (err) => {
        console.error('Error fetching user profile:', err);
      },
    });
  }

  goToProfile(): void {
    this.router.navigate(['/profile']);
  }

  goToMyRentals(): void {
    console.log('You are in the reservation screen');
  }

  openReturnModal(rental: Rental): void {
    this.selectedRental = rental;
    this.isReturnModalVisible = true;
  }

  openCancelModal(rental: Rental): void {
    this.selectedRental = rental;
    this.cancelReason = '';
    this.isCancelModalVisible = true;
  }

  closeModal(): void {
    this.isReturnModalVisible = false;
    this.isCancelModalVisible = false;
  }

  confirmReturn(): void {
    const params = new HttpParams()
    .set('slug', this.selectedRental.slug)
    .set('latitude', this.returnData.latitude)
    .set('longtitude', this.returnData.longtitude)
    .set('description', this.returnData.feedback);

    this.client
      .put(`${this.baseUrl}/Rental/return-rental`, null, { params })
      .subscribe({
        next: () => {
          console.log('return data:', this.returnData);
          console.log(`Vehicle ${this.selectedRental.vin} was returned.`);
        },
        error: (err) => {
          console.error('Error return:', err);
        },
      });
    this.closeModal();
  }

  confirmCancel(): void {
    // TODO: wyslac anulowanie rezerwacji
    console.log(
      `car reservation ${this.selectedRental.vin} was cancel.`
    );
    this.closeModal();
  }

  getStatusText(status: string): string {
    switch (status) {
      case 'Pending':
        return 'Pending Approval';
      case 'ConfirmedByUser':
        return 'Confirmed';
      case 'CompletedByEmployee':
        return 'Completed';
      case 'WaitingForReturnAcceptance':
        return 'Waiting for Return Acceptance';
      case 'Returned':
        return 'Returned';
      default:
        return 'Unknown';
    }
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Pending':
        return 'status-pending';
      case 'ConfirmedByUser':
        return 'status-confirmed';
      case 'CompletedByEmployee':
        return 'status-completed';
      case 'WaitingForReturnAcceptance':
        return 'status-waiting';
      case 'Returned':
        return 'status-returned';
      default:
        return 'status-unknown';
    }
  }
}
