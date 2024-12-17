using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using search.api.DTOs;
using search.api.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace search.api.Services;

public class EmailService : IEmailInterface
{
    private readonly string _apiKey = "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";

    private readonly string _searchName = "SuperSamochodziki";
    
    public async Task SendRentalConfirmationEmailAsync(string toUser, string subject, string message, 
                                                       string username, int rentId, string confirmationLink)
    {
        var apiKey = _apiKey;
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("mateusz.mm100@gmail.com", _searchName);
        var to = new EmailAddress(toUser, username);
        var plainTextContent = message;
        var htmlContent = $@"
        <html>
            <body>
                <h1>Rental Confirmation</h1>
                <p>Dear {username},</p>
                <p>{message}</p>
                <p>Your rental {rentId} is waiting for your confirmation!</p>
                <p>To validate your rental please click in <a href={confirmationLink}>this link.</a><br /><br /></p>               
                <p>Thank you for using {_searchName}!</p>
                <footer>
                    <p>--<br/>{_searchName} Team</p>
                </footer>
            </body>
        </html>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
    }

    public async Task SendRentalCompletionEmailAsync(string toUser, string subject, string username, int rentId, string message)
    {
        var apiKey = _apiKey;
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("mateusz.mm100@gmail.com", _searchName);
        var to = new EmailAddress(toUser, username);
        var plainTextContent = message;
        var htmlContent = $@"
        <html>
            <body>
                <h1>Rental Confirmation</h1>
                <p>Dear {username},</p>
                <p>{message}</p>               
                <p>Thank you for using {_searchName}!</p>
                <footer>
                    <p>--<br/>{_searchName} Team</p>
                </footer>
            </body>
        </html>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
    }
    
    public string GenerateConfirmationRentToken(string email, string username, int id, int rentId, IConfiguration configuration)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim("RentalId", rentId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT_KEY"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JWT_ISSUER"],
            audience: configuration["JWT_AUDIENCE"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}