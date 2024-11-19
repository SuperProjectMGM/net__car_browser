using System;
using System.Text.Json.Serialization;

namespace search.api.DTOs
{
    public class VehicleOurDto
    {
        [JsonPropertyName("carId")]
        public int CarId { get; set; } // Integer ID, żeby jednoznacznie reprezentować identyfikator pojazdu

        [JsonPropertyName("brand")]
        public string Brand { get; set; } = ""; // Marka jako string, bo jest to tekstowa właściwość

        [JsonPropertyName("model")]
        public string Model { get; set; } = ""; // Model również tekstowy

        [JsonPropertyName("yearOfProduction")]
        public int YearOfProduction { get; set; } // Rok produkcji to liczba całkowita, więc używamy int

        [JsonPropertyName("type")]
        public string Type { get; set; } = ""; // Typ pojazdu jako string (np. "sedan", "SUV")

        [JsonPropertyName("rentalFrom")]
        public DateTime RentalFrom { get; set; } // Data początkowa wynajmu, reprezentowana jako DateTime

        [JsonPropertyName("rentalTo")]
        public DateTime RentalTo { get; set; } // Data końcowa wynajmu, również jako DateTime

        [JsonPropertyName("prize")]
        public decimal Prize { get; set; } // Cena wynajmu jako decimal (lepsze do przechowywania wartości finansowych)

        [JsonPropertyName("driveType")]
        public string DriveType { get; set; } = ""; // Typ napędu (np. "AWD", "FWD", "RWD")

        [JsonPropertyName("transmission")]
        public string Transmission { get; set; } = ""; // Typ skrzyni biegów (np. "manual", "automatic")

        [JsonPropertyName("description")]
        public string Description { get; set; } = ""; // Opis pojazdu jako string

        [JsonPropertyName("rate")]
        public decimal Rate { get; set; } // Stawka wynajmu, również jako decimal

        [JsonPropertyName("localization")]
        public string Localization { get; set; } = ""; // Lokalizacja pojazdu (np. "Warszawa", "Kraków")

        [JsonPropertyName("serialNo")]
        public string SerialNo { get; set; } = ""; // Numer seryjny pojazdu

        [JsonPropertyName("vinId")]
        public string VinId { get; set; } = ""; // Numer VIN pojazdu, jako string

        [JsonPropertyName("registryNo")]
        public string RegistryNo { get; set; } = ""; // Numer rejestracyjny pojazdu, jako string
    }
}

