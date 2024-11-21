import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-confirm-rent',
  standalone: true,
  templateUrl: './confirm-rent.component.html',
  styleUrls: ['./confirm-rent.component.css']
})
export class ConfirmRentComponent {
  @Output() closeModalEvent = new EventEmitter<void>(); // Wydarzenie do powiadomienia o zamknięciu

  close() {
    this.closeModalEvent.emit(); // Emitujemy zdarzenie do zamknięcia
  }
}
