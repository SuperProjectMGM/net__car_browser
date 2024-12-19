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
    private readonly string _searchName = "SuperSamochodziki";

    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task SendRentalConfirmationEmailAsync(string toUser, string username, string slug, string confirmationLink)
    {
        var apiKey = _configuration["SEND_GRID_API_KEY"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(_configuration["MAIL_SENDER"], _searchName);
        var to = new EmailAddress(toUser, username);
        var plainTextContent = "";
        string subject = "Rental confirmation";
        var htmlContent = $@"
        <html>
            <body>
                <h1>Rental Confirmation</h1>
                <p>Dear {username},</p>
                <p>{plainTextContent}</p>
                <p>Your rental {slug} is waiting for your confirmation!</p>
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

    public async Task SendRentalCompletionEmailAsync(string toUser, string username, string rentSlug)
    {
        var apiKey = _configuration["SEND_GRID_API_KEY"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(_configuration["MAIL_SENDER"], _searchName);
        var to = new EmailAddress(toUser, username);
        var plainTextContent = "Your rental has been completed successfully!";
        string subject = "Rental completion!";
        
        var htmlContent = $@"
        <html>
            <body>
                <h1>Rental Confirmation</h1>
                <p>Dear {username},</p>
                <p>{plainTextContent}</p>               
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
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}