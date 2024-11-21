import { Component, Input, SimpleChanges } from '@angular/core';
import { environment } from '../../../environments/environment';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ProfileService } from '../../services/profile.service';
import { VehicleDetail } from '../../models/VehicleDetail.model';

@Component({
  selector: 'app-cars-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cars-list.component.html',
  styleUrl: './cars-list.component.css',
})
export class CarsListComponent {
  ApiUrl = environment.apiBaseUrl;
  @Input() cars: VehicleDetail[] = [];
  @Input() pickupDateTime: Date | null = null;
  @Input() returnDateTime: Date | null = null;

  constructor(
    private profileSerive: ProfileService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnChanges(changes: SimpleChanges) {
    if (changes['cars']) {
      console.log('Nowe dane samochodów:', this.cars);
    }
  }

  rentCar(car: VehicleDetail): void {
    if (this.authService.isLoggedIn()) {
      this.profileSerive.isUserProfileComplete().subscribe((isComplete) => {
        if (isComplete) {
          console.log(
            'Chęć wynajęcia fury:',
            car.model,
            'w okresie:',
            this.pickupDateTime,
            ' - ',
            this.returnDateTime
          );
          this.authService.wantRentVehicle(car, this.pickupDateTime, this.returnDateTime).subscribe(
            (response) => {
              // Obsługuje udaną odpowiedź
              console.log('Rent request successful', response);// TODO: co na frocie po wysłaniu
            },
            (error) => {
              // Obsługuje błąd
              console.error('Error during vehicle rent request', error);// TODO: co dalej?
              
            }
          );
        } else {
          console.log('Nie ma wypełnionych danych profilu.');
          this.router.navigate(['/edit-profile']);
        }
      });
    } else {
      console.log('Użytkownik nie jest zalogowany.');
      this.router.navigate(['/login']);
    }
  }
}
