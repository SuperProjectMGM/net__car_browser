namespace search.api.Messages;

public class UserReturn
{
    public string Slug { get; set; } = string.Empty;
    public float? Longtitude { get; set; } 
    public float? Latitude { get; set; }
    public string Description { get; set; } = string.Empty;
}