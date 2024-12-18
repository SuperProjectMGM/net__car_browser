using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;

namespace search.api.Models;

public class UserDetails : IdentityUser<int>
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; } 
    public string DrivingLicenseNumber { get; set; } = string.Empty;
    public DateTime DrivingLicenseIssueDate { get; set; }
    public DateTime DrivingLicenseExpirationDate { get; set; }
    public string StreetAndNumber { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PersonalNumber { get; set; } = string.Empty;
    public string IdCardIssuedBy { get; set; } = string.Empty;
    public DateTime IdCardIssueDate { get; set; } 
    public DateTime IdCardExpirationDate { get; set; } 
}