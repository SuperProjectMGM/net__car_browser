using search.api.DTOs;

public class PriceRequestDto
{
    public VehicleOurDto Vehicle { get; set; }
    public UserDto User { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}