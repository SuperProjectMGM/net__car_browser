using System.Runtime.InteropServices.JavaScript;

namespace search.api.DTOs;

public class VehicleRentRequestDto
{
    public string VehicleVin { get; set; } = string.Empty;

    public string CarProviderIdentifier { get; set; } = string.Empty;
    
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }

    public string Description { get; set; } = string.Empty;
}