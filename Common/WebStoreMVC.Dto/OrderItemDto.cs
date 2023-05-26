namespace WebStoreMVC.Dto;
public class OrderItemDto
{
	public int Id { get; init; }
	public ProductDto Product { get; init; } = null!;
	public decimal Price { get; init; }
	public int Quantity { get; init; }
}