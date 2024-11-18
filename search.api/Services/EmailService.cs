using search.api.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace search.api.Services;

public class EmailService : IEmailInterface
{
    private readonly string _API_KEY = "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";
    
    public async Task SendRentalConfirmationEmailAsync(string subject, string toUser, string message)
    {
        var apiKey = _API_KEY;
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("mateusz.mm100@gmail.com", "SearchCar");
        var to = new EmailAddress(toUser, "");
        var plainTextContent = message;
        var htmlContent = "";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
    }
}