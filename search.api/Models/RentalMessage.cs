using search.api.Repositories;

namespace search.api.Models;

public class RentalMessage
{
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;
    
    public DateTime BirthDate { get; set; }
    
    public DateTime DateOfReceiptOfDrivingLicense { get; set; }
    
    public int PersonalNumber { get; set; }
    
    public int LicenceNumber { get; set; }
    
    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    
    public string VinId { get; set; } = string.Empty;
    
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
    
    public RentalRepository.RentalStatus Status { get; set; }

    public string Description { get; set; } = string.Empty;
}