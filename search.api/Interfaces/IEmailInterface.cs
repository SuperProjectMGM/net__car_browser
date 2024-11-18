namespace search.api.Interfaces;

public interface IEmailInterface
{
    public Task SendRentalConfirmationEmailAsync(string subject, string toUser, string message);
}