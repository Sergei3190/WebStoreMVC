namespace WebStoreMVC.Dto;

public class ProductDto
{
	public int Id { get; init; }
	public string Name { get; init; } = null!;
	public int Order { get; init; }
	public string ImageUrl { get; set; } = null!;
	public decimal Price { get; set; }
	public SectionDto Section { get; set; } = null!;
	public BrandDto? Brand { get; set; }
}
