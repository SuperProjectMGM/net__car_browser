import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Rental } from '../../models/RentalModel.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-my-rentals',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './my-rentals.component.html',
  styleUrls: ['./my-rentals.component.css'],
})
export class MyRentalsComponent implements OnInit {
  rentals: Rental[] = [];

  isReturnModalVisible = false;
  isCancelModalVisible = false;
  selectedRental!: Rental;

  returnData = {
    latitude: '',
    longitude: '',
    feedback: '',
  };
  cancelReason = '';

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.fetchRentals();
  }

  fetchRentals(): void {
    // TODO: zrobic impelemetacje pobrania rezerwacji
    this.rentals = [
      {
        car: 'Toyota Corolla',
        startDate: '2024-04-01',
        endDate: '2024-04-10',
        status: 'W trakcie',
      },
      {
        car: 'Ford Focus',
        startDate: '2024-03-20',
        endDate: '2024-03-25',
        status: 'Zakończone',
      },
      {
        car: 'BMW 3 Series',
        startDate: '2024-04-15',
        endDate: '2024-04-20',
        status: 'Nadchodzące',
      },
    ];
  }

  goToProfile(): void {
    this.router.navigate(['/profile']);
  }

  goToMyRentals(): void {
    console.log('Już znajdujesz się na zakładce Moje Rezerwacje');
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
    // TODO: wyslac zwrot
    console.log('Dane zwrotu:', this.returnData);
    console.log(`Samochód ${this.selectedRental.car} został zwrócony.`);
    this.closeModal();
  }

  confirmCancel(): void {
    // TODO: wyslac anulowanie rezerwacji
    console.log(
      `Rezerwacja samochodu ${this.selectedRental.car} została anulowana.`
    );
    console.log('Przyczyna rezygnacji:', this.cancelReason);
    this.closeModal();
  }
}
