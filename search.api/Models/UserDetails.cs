using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;

namespace search.api.Models;

public class UserDetails : IdentityUser
{
    public string? Name { get; set; } 
    public string? Surname { get; set; } 
    public DateOnly? BirthDate { get; set; } 
    public string? DrivingLicenseNumber { get; set; } 
    public DateOnly? DrivingLicenseIssueDate { get; set; }
    public DateOnly? DrivingLicenseExpirationDate { get; set; }
    public string? StreetAndNumber { get; set; } 
    public string? PostalCode { get; set; } 
    public string? City { get; set; } 
    public string? PersonalNumber { get; set; } 
    public string? IdCardIssuedBy { get; set; } 
    public DateOnly? IdCardIssueDate { get; set; } 
    public DateOnly? IdCardExpirationDate { get; set; } 
}