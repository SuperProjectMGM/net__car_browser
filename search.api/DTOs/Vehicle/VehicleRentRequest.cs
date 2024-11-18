using System.Runtime.InteropServices.JavaScript;

namespace search.api.DTOs;

public class VehicleRentRequest
{
    public int CarId { get; set; }
    
    public DateTime StartRent { get; set; }
    
    public DateTime EndRent { get; set; }

    public string RentMessage { get; set; } = String.Empty;
}