public class KEJRentalOfferDto
{
    public int Id { get; set; }
    public VehicleKEJDto Car { get; set; }
    public decimal DailyRate { get; set; }
    public decimal InsuranceRate { get; set; }
    public decimal TotalCost { get; set; }
    public DateTime ValidUntil { get; set; }
}