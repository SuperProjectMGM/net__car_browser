using System;
using System.Text.Json.Serialization;

namespace search.api.DTOs
{
    public class VehicleOurDto
    {
        [JsonPropertyName("brand")]
        public string Brand { get; set; } = string.Empty; 

        [JsonPropertyName("model")]
        public string Model { get; set; } = string.Empty; 

        [JsonPropertyName("yearOfProduction")]
        public int YearOfProduction { get; set; } 

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; } 

        [JsonPropertyName("driveType")]
        public string DriveType { get; set; } = string.Empty;

        [JsonPropertyName("transmission")]
        public string Transmission { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty; 

        [JsonPropertyName("rate")]
        public float Rate { get; set; }

        [JsonPropertyName("localization")]
        public string Localization { get; set; } = string.Empty;

        [JsonPropertyName("serialNo")]
        public string SerialNo { get; set; } = string.Empty;

        [JsonPropertyName("vin")]
        public string Vin { get; set; } = string.Empty;

        [JsonPropertyName("registryNo")]
        public string RegistryNo { get; set; } = string.Empty; 
    }
}

