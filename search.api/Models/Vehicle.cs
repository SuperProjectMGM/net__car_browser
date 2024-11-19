using search.api.Models;

public class Vehicle
{
    public int CarId { get; set; }

    public string Brand { get; set; } = "";
    public string Model { get; set; } = "";
    public string SerialNo { get; set; } = "";
    public string VinId { get; set; } = "";
    public string RegistryNo { get; set; } = "";
    public int YearOfProduction { get; set; }
    public DateTime RentalFrom { get; set; }
    public DateTime RentalTo { get; set; }
    public decimal Prize { get; set; }
    public string DriveType { get; set; } = "";
    public string Transmission { get; set; } = "";
    public string Description { get; set; } = "";
    public string Type { get; set; } = "";
    public decimal Rate { get; set; }
    public string Localization { get; set; } = "";
    public RentalFirm RentalFirm { get; set; } = new RentalFirm();
}