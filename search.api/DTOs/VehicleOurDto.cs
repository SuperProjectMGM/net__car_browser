using System;
using System.Text.Json.Serialization;

namespace search.api.DTOs
{
    public class VehicleOurDto
    {
        [JsonPropertyName("brand")]
        public string Brand { get; set; } = "No data provided"; 

        [JsonPropertyName("model")]
        public string Model { get; set; } = "No data provided"; 

        [JsonPropertyName("yearOfProduction")]
        public int YearOfProduction { get; set; } 

        [JsonPropertyName("type")]
        public string Type { get; set; } = "No data provided";

        [JsonPropertyName("price")]
        public decimal Price { get; set; } 

        [JsonPropertyName("driveType")]
        public string DriveType { get; set; } = "No data provided";

        [JsonPropertyName("transmission")]
        public string Transmission { get; set; } = "No data provided";

        [JsonPropertyName("description")]
        public string Description { get; set; } = "No data provided"; 

        [JsonPropertyName("rate")]
        public float Rate { get; set; }

        [JsonPropertyName("localization")]
        public string Localization { get; set; } = "No data provided";

        [JsonPropertyName("serialNo")]
        public string SerialNo { get; set; } = "No data provided";

        [JsonPropertyName("vin")]
        public string Vin { get; set; } = "No data provided";

        [JsonPropertyName("registryNo")]
        public string RegistryNo { get; set; } = "No data provided"; 
        public string RentalFirmName { get; set; } = "MGMCO";
        public string PhotoUrl { get; set; } = "https://returnimages.blob.core.windows.net/vehicles/vehicles/zygzag.jpg";
    }
}

