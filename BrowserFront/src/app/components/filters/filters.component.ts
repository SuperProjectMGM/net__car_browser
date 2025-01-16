import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CarsService } from '../../services/cars.service';

@Component({
  selector: 'app-filters',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.css'],
})
export class FiltersComponent {
  @Output() filtersChanged = new EventEmitter<any>();

  constructor(private carsService: CarsService) {}

  selectedFilters: any = {
    price: [],
    driveType: [],
    transmission: [],
    rate: [],
    brand: [],
    model: [],
  };

  brands: string[] = [
    'Toyota',
    'BMW',
    'Mercedes',
    'Peugeot',
    'Citroen',
    'Volkswagen',
    'Fiat',
    'Porsche',
    'Ford',
    'Audi',
    'Hyundai',
    'Kia',
    'Honda',
    'Volvo',
    'Alfa Romeo',
    'Skoda',
  ];

  models: string[] = [];

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

    if (filterGroup === 'brand') {
      this.updateModels();
    }

    this.onFilterChange();
  }

  updateModels(): void {
    const selectedBrand = this.selectedFilters.brand[0];
    this.models = [];

    if (selectedBrand) {
      this.carsService.getAvailableCars().subscribe((availableCars) => {
        if (!availableCars || availableCars.length === 0) {
          console.warn('No available cars to update models.');
          return;
        }
        this.models = availableCars
          .filter((car) => car.brand === selectedBrand)
          .map((car) => car.model);
        this.models = [...new Set(this.models)];
      });
    }
  }

  onModelChange(event: Event) {
    const selectedModel = (event.target as HTMLSelectElement).value;
    this.selectedFilters.model = selectedModel ? [selectedModel] : [];
    this.onFilterChange();
  }
}
