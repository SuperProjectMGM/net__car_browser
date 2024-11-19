namespace search.api.Interfaces;

public interface IEmailInterface
{
    public Task SendRentalConfirmationEmailAsync(string toUser, string subject, string message,
        string username, string confirmationLink, string startDate, string endDate);

    public string GenerateConfirmationRentToken(string email, string username, string id, string rentId, IConfiguration configuration);
}