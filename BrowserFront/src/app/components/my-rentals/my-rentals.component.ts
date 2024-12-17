import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Rental } from '../../models/RentalModel.model';

@Component({
  selector: 'app-my-rentals',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './my-rentals.component.html',
  styleUrls: ['./my-rentals.component.css'],
})
export class MyRentalsComponent implements OnInit {
  rentals: Rental[] = []; // Deklaracja z użyciem zaimportowanego interfejsu

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.fetchRentals();
  }

  fetchRentals(): void {
    // TODO: call na backend o wypozyczenia
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

  returnCar(rental: Rental): void {
    console.log(`Samochód ${rental.car} został zwrócony.`);
    // Dodaj logikę zwrotu samochodu (np. zmiana statusu lub wywołanie API)
  }

  cancelReservation(rental: Rental): void {
    console.log(`Rezerwacja samochodu ${rental.car} została anulowana.`);
    // Dodaj logikę anulowania rezerwacji (np. usunięcie lub aktualizacja rezerwacji)
  }
}
