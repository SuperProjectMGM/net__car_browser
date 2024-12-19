import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { ProfileComponent } from './components/profile/profile.component';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';
import { MyRentalsComponent } from './components/my-rentals/my-rentals.component';
import { ViewDealComponent } from './components/view-deal/view-deal.component';

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

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
