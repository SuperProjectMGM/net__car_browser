using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;

namespace search.api.Models;

public class UserDetails : IdentityUser
{
    public string? Name { get; set; } 
    public string? Surname { get; set; } 
    public string? BirthDate { get; set; } 
    public string? DrivingLicenseNumber { get; set; } 
    public string? DrivingLicenseIssueDate { get; set; }
    public string? DrivingLicenseExpirationDate { get; set; }
    public string? AddressStreet { get; set; } 
    public string? PostalCode { get; set; } 
    public string? City { get; set; } 
    public string? IdPersonalNumber { get; set; } 
    public string? IdCardIssuedBy { get; set; } 
    public string? IdCardIssueDate { get; set; } 
    public string? IdCardExpirationDate { get; set; } 
}