import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-filters',
  standalone: true,
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.css'],
})
export class FiltersComponent {
  @Output() filtersChanged = new EventEmitter<any>();

  selectedFilters: any = {
    price: [],
    driveType: [],
    transmission: [],
    rate: [],
  };

  onFilterChange() {
    this.filtersChanged.emit(this.selectedFilters);
  }

  toggleArrayFilter(filterGroup: string, value: string) {
    const group = this.selectedFilters[filterGroup];
    if (group.includes(value)) {
      this.selectedFilters[filterGroup] = group.filter(
        (v: string) => v !== value
      );
    } else {
      this.selectedFilters[filterGroup].push(value);
    }
    this.onFilterChange();
  }
}
