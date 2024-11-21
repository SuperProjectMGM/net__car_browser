using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

public class RegisterModel
{
    [Required(ErrorMessage = "Email Name is required!")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Password is required!")]
    public string? Password { get; set; } 
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? UserName { get; set; } 
    public string? PhoneNumber { get; set; }
    public string? AddressStreet {get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? DateOfBirth { get; set; }
    public string? DrivingLicenseNumber { get; set; }
    public string? DrivingLicenseIssueDate { get; set; }
    public string? DrivingLicenseExpirationDate { get; set; } 
    public string? IdCardNumber { get; set; }
    public string? IdCardIssuedBy { get; set; }
    public string? IdCardIssueDate { get; set; }
    public string? IdCardExpirationDate { get; set; }
}