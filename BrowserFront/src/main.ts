import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter, Routes } from '@angular/router';
import { LoginComponent } from './app/components/login/login.component';
import { DashboardComponent } from './app/components/dashboard/dashboard.component';
import { RegistrationComponent } from './app/components/registration/registration.component';
import { provideHttpClient } from '@angular/common/http';
import { ProfileComponent } from './app/components/profile/profile.component';
import { EditProfileComponent } from './app/components/edit-profile/edit-profile.component';
import { MyRentalsComponent } from './app/components/my-rentals/my-rentals.component';
import { ViewDealComponent } from './app/components/view-deal/view-deal.component';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegistrationComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'edit-profile', component: EditProfileComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'my-rentals', component: MyRentalsComponent },
  { path: 'view-deals', component: ViewDealComponent },
  { path: '**', redirectTo: 'dashboard' },
];

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    provideAnimations(), // Wymagane przez ngx-toastr
    provideToastr({
      positionClass: 'toast-top-right', // Ustawienie pozycji na prawy górny róg
      timeOut: 3000, // Czas wyświetlania komunikatu (3 sekundy)
      closeButton: true, // Dodanie przycisku zamknięcia
    }),
  ],
}).catch((err) => console.error(err));
// PaeGh6ei!
