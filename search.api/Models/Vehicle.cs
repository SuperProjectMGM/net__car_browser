using search.api.Models;

public class Vehicle
{
    public int CarId { get; set; } // Id of a car. I don't really know if it must be an id from another api.
    public string Brand { get; set; }
    public string Model { get; set; }
    public string YearOfProduction {get; set; } // It shouldn't be string. But now I know, that in ours api now it is string.
    public string Type { get; set; }
    public RentalFirm RentalFirm { get; set; } // A firm that gives us that car
}