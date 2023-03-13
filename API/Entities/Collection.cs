namespace API.Entities;

public class Collection
{
    public int CollectionId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<Product>? Products { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}