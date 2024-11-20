using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;

namespace search.api.Models;

public class UserDetails : IdentityUser
{
    
    public string Name { get; set; } = String.Empty;
    public string Surname { get; set; } = String.Empty;
    public string BirthDate { get; set; } = String.Empty;
    public string IssueDateDrivingLicense { get; set; } = String.Empty;
    public string ExpirationDateDrivingLicense { get; set; } = String.Empty;
    public string LicenseNumber { get; set; } = String.Empty;
    public string AddressStreet { get; set; } = String.Empty;
    public string PostalCode { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;
    public string PersonalNumber { get; set; } = String.Empty;
    public string IssueDateIdCard { get; set; } = String.Empty;
    public string ExpirationDateIdCard { get; set; } = String.Empty;
}