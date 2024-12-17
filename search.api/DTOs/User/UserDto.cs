public class UserDto
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string DrivingLicenseNumber { get; set; } = string.Empty;
    public DateOnly DrivingLicenseIssueDate { get; set; }
    public DateOnly DrivingLicenseExpirationDate { get; set; }
    public string StreetAndNumber { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PersonalNumber { get; set; } = string.Empty;
    public string IdCardIssuedBy { get; set; } = string.Empty;
    public DateOnly IdCardIssueDate { get; set; }
    public DateOnly IdCardExpirationDate { get; set; }   
}