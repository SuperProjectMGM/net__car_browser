using System.Runtime.InteropServices.JavaScript;

namespace search.api.Models;

public class UserDetails
{
    public string Id { get; set; } = string.Empty;
    
    // UserId from AspUsersNet
    public string UserId { get; set; } = string.Empty;
    
    public string Name { get; set; } = String.Empty;

    public string Surname { get; set; } = String.Empty;
    
    public DateTime BirthDate { get; set; }
    
    public DateTime DateOfReceiptOfDrivingLicense { get; set; }
    
    public int PersonalNumber { get; set; }
    
    public int LicenceNumber { get; set; }
    
    public string Address { get; set; } = String.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}