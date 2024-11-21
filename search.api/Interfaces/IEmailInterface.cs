namespace search.api.Interfaces;

public interface IEmailInterface
{
    public Task SendRentalConfirmationEmailAsync(string toUser, string subject, string message,
        string username, string rentId, string confirmationLink);

    public string GenerateConfirmationRentToken(string email, string username, string id, string rentId, IConfiguration configuration);

    public Task SendRentalCompletionEmailAsync(string toUser, string subject, string username, string rentId,
        string message);
}