import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ConfirmRentComponent } from './components/confirm-rent/confirm-rent.component';
import { MyRentalsComponent } from './components/my-rentals/my-rentals.component';

@NgModule({
  declarations: [],

  imports: [BrowserModule, AppRoutingModule, HttpClientModule, FormsModule],
  providers: [],
  bootstrap: [],
})
export class AppModule {}
