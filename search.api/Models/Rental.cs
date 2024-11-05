namespace search.api.Models;

public class Rental
{
    public string Id { get; set; } = string.Empty;
    
    public int? UserId { get; set; }
    
    public User? User { get; set; }
    
    public RentalFirm? RentalFirm { get; set; }
    
    public int VinId { get; set; }
    
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
    
    public string Status { get; set; } = String.Empty;

    public string Description { get; set; } = string.Empty;
}