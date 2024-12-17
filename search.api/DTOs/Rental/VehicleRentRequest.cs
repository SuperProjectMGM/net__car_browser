using System.Runtime.InteropServices.JavaScript;

namespace search.api.DTOs;

public class VehicleRentRequest
{
    public string VehicleVin { get; set; } = string.Empty;

    public int RentalFirmId { get; set; }
    
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }

    public string Description { get; set; } = string.Empty;
}