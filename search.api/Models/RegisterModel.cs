using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

public class RegisterModel
{
    [Required(ErrorMessage = "Email Name is required!")]
    public string? Email { get; set; } = String.Empty;

    [Required(ErrorMessage = "Password is required!")]
    public string? Password { get; set; } = String.Empty;
    
    public string? UserName { get; set; } = String.Empty;
}