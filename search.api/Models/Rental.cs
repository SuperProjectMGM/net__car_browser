using search.api.Repositories;

namespace search.api.Models;

public class Rental
{
    public int Id { get; set; } 
    public string Slug { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string CarProviderIdentifier { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public RentalStatus Status { get; set; }
    public string Description { get; set; } = string.Empty;
}

public enum RentalStatus
{
    Pending = 1,  
    ConfirmedByUser = 2, 
    CompletedByEmployee = 3,
    WaitingForReturnAcceptance = 4,
    Returned = 5
}