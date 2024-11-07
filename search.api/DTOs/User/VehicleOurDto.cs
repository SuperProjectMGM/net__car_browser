using System.Text.Json.Serialization;

namespace search.api.DTOs;

public class VehicleOurDto
{
    [JsonPropertyName("carId")]
    public int CarId { get; set; } 
    [JsonPropertyName("brand")]
    public string Brand { get; set; } = "";
    [JsonPropertyName("model")]
    public string Model { get; set; } = "";
    [JsonPropertyName("yearOfProduction")]
    public string YearOfProduction {get; set; }  = "";
    [JsonPropertyName("type")]
    public string Type { get; set; } = "";
}