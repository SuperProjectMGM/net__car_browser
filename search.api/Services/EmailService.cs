using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using search.api.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace search.api.Services;

public class EmailService : IEmailInterface
{
    private readonly string _apiKey = "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";

    private readonly string _searchName = "SuperSamochodziki";
    
    public async Task SendRentalConfirmationEmailAsync(string toUser, string subject, string message, 
                                                       string username, string confirmationLink, string startDate,
                                                       string endDate)
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
                <p>Your rental is valid from {startDate} to {endDate}.</p>
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
    
    public string GenerateConfirmationToken(string email, string username, string id, IConfiguration configuration)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, id)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JWT:ValidIssuer"],
            audience: configuration["JWT:ValidAudience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}