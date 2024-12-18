namespace search.api.Interfaces;

public interface IEmailInterface
{
    public Task SendRentalConfirmationEmailAsync(string toUser, string username, string slug, string confirmationLink);

    public string GenerateConfirmationRentToken(string email, string username, int id, int rentId, IConfiguration configuration);

    public Task SendRentalCompletionEmailAsync(string toUser, string username, string rentSlug);
}