public class SearchInfo
{
      public string UserId { get; set; } = string.Empty; // User that makes a search
      public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}