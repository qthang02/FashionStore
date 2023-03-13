namespace API.DTOs;

public class CollectionDto
{
    public int CollectionId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<ProductDto>? Products { get; set; }
}