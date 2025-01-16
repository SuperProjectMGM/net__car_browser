namespace search.api.DTOs;

public class KEJRental
{
    public int OfferId;
    public int CustomerId;
    public string RentalName { get; set; } = string.Empty;
    public DateTime PlannedStartDate { get; set; }
    public DateTime PlannedEndDate { get; set; }
}