namespace search.api.Interfaces;

public interface IEmailInterface
{
    public Task ConfirmationEmailAsync(string toUser, string username, string slug, string confirmationLink);
    public Task CompletionEmailAsync(string toUser, string username, string rentSlug);

    public Task ReturnEmailAsync(string toUser, string username, float paymentDue, string rentSlug);

    public string GenerateConfirmationRentToken(string email, string username, int id, int rentId, IConfiguration configuration);

}