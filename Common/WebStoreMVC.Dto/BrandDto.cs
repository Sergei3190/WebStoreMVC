namespace WebStoreMVC.Dto;
public class BrandDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public int Order { get; init; }
	public int ProductsCount { get; set; }
}