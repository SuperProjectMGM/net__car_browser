using System.Runtime.InteropServices.JavaScript;

namespace search.api.DTOs;

public class VehicleRentRequest
{
    public string VehicleVin { get; set; } = string.Empty;

    public string RentalFirmId { get; set; } = string.Empty;
    
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
}