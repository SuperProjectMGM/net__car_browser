using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using search.api.Models;

public class Vehicle
{
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string SerialNo { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public string RegistryNo { get; set; } = string.Empty;
    public int YearOfProduction { get; set; }
    public decimal Price { get; set; }
    public string DriveType { get; set; } = string.Empty;
    public string Transmission { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    
    [Column(TypeName = "money")]
    public float Rate { get; set; }
    public string Localization { get; set; } = string.Empty;
    public string RentalFirmName { get; set; } = string.Empty; 
    public string PhotoUrl { get; set; } = string.Empty;
}