using System.Runtime.InteropServices.JavaScript;

namespace search.api.Models;

public class User
{
    public int Id { get; set; }
    
    public string Name { get; set; } = String.Empty;

    public string Surname { get; set; } = String.Empty;
    
    public DateTime BirthDate { get; set; }
    
    public DateTime LicenceDate { get; set; }
    
    public int PersonalId { get; set; }
    
    public int LicenceId { get; set; }

    public string Email { get; set; } = String.Empty;
    
    public string Address { get; set; } = String.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}