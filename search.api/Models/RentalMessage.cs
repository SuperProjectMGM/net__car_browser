using search.api.Repositories;

namespace search.api.Models;

public class RentalMessage
{
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public DateOnly DrivingLicenseIssueDate { get; set; }
    public string PersonalNumber { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;    
    public string City { get; set; } = string.Empty;
    public string StreetAndNumber { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public RentalStatus Status { get; set; }
    public string Description { get; set; } = string.Empty;
}


public enum RentalStatus
{
    Pending = 1,    // Rental request is pending
    Confirmed = 2,  // Rental has been confirmed
    Completed = 3,  // Rental has been completed
}
    