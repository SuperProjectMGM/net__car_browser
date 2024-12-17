namespace search.api.Interfaces;

public interface IEmailInterface
{
    public Task SendRentalConfirmationEmailAsync(string toUser, string subject, string message,
        string username, int rentId, string confirmationLink);

    public string GenerateConfirmationRentToken(string email, string username, int id, int rentId, IConfiguration configuration);

    public Task SendRentalCompletionEmailAsync(string toUser, string subject, string username, int rentId,
        string message);
}