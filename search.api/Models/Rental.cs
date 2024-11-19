using search.api.Repositories;

namespace search.api.Models;

public class Rental
{
    public string Id { get; set; } = string.Empty;
    
    public string UserId { get; set; } = string.Empty;

    public string RentalFirmId { get; set; } = string.Empty;

    public string VinId { get; set; } = string.Empty;
    
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
    
    public RentalRepository.RentalStatus Status { get; set; }

    public string Description { get; set; } = string.Empty;
}