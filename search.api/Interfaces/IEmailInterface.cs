namespace search.api.Interfaces;

public interface IEmailInterface
{
    public Task SendRentalConfirmationEmailAsync(string toUser, string subject, string message,
        string username, string confirmationLink, string startDate, string endDate);

    public string GenerateConfirmationToken(string email, string username, string id, IConfiguration configuration);
}