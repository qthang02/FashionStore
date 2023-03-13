namespace API.Entities;

public class Photo
{
    public int PhotoId { get; set; }
    public string? Url { get; set; }
    public string? PublishUrl { set; get; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}