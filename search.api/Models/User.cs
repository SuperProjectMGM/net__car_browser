using System.Runtime.InteropServices.JavaScript;

namespace search.api.Models;

public class User
{
    public string Id { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    
    public string Name { get; set; } = String.Empty;

    public string Surname { get; set; } = String.Empty;
    
    public DateTime BirthDate { get; set; }
    
    public DateTime DateOfReceiptOfDrivingLicense { get; set; }
    
    public int PersonalNumber { get; set; }
    
    public int LicenceNumber { get; set; }

    public string Email { get; set; } = String.Empty;
    
    public string Address { get; set; } = String.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}