using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

public class RegisterModel
{
    [Required(ErrorMessage = "Email Name is required!")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is required!")]
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string SecondName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string StreetAndNumber {get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string DrivingLicenseNumber { get; set; } = string.Empty;
    public DateOnly DrivingLicenseIssueDate { get; set; }
    public DateOnly DrivingLicenseExpirationDate { get; set; } 
    public string PersonalNumber { get; set; } = string.Empty;
    public string IdCardIssuedBy { get; set; } = string.Empty;
    public DateOnly IdCardIssueDate { get; set; }
    public DateOnly IdCardExpirationDate { get; set; }
}