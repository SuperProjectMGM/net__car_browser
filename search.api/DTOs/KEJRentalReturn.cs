public class KEJRentalReturn
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public int UserId { get; set; } 
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }
    public string StartLocation { get; set; }
    public string EndLocation { get; set; }
}