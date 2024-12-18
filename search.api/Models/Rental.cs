using search.api.Repositories;

namespace search.api.Models;

public class Rental
{
    public int Id { get; set; } 
    public string Slug { get; set; } = string.Empty;
    public int UserId { get; set; }
    
    // Temporary, till we figure out how we are supposed to handle multiple vehicle providers logic
    //public int RentalFirmId { get; set; }
    public string Vin { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public RentalStatus Status { get; set; }
    public string Description { get; set; } = string.Empty;
}