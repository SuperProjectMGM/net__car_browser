namespace search.api.DTOs;

public class KEJRental
{
    public int OfferId { get; set; }
    public int CustomerId { get; set; }
    public string RentalName { get; set; } = string.Empty;
    public DateTime PlannedStartDate { get; set; }
    public DateTime PlannedEndDate { get; set; }
}