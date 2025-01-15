export interface VehicleRentRequest {
    vehicleVin: string;
    carProviderIdentifier: string;
    start: Date;
    end: Date;
    description: string;
  }
  