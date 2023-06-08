using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Dto;

public class ProductDto
{
	public int Id { get; init; }
	public string Name { get; init; } = null!;
	public int Order { get; init; }
	public string ImageUrl { get; set; } = null!;
	public decimal Price { get; set; }
	public int SectionId { get; set; }
	public int? BrandId { get; set; }
	public SectionDto? Section { get; set; }
	public BrandDto? Brand { get; set; }
}
