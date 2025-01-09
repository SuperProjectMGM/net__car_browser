using search.api.Models;

public class RentalDto
{
    public string Slug { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Status { get; set; }
    public string Description { get; set; } = string.Empty;
}