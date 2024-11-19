using System.ComponentModel.DataAnnotations;

public class RegisterModel
{
    [Required(ErrorMessage = "Email Name is required!")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Password is required!")]
    public string? Password {get; set; }
}