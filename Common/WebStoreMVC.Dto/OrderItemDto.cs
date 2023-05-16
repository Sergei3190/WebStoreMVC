namespace WebStoreMVC.Dto;
public class OrderItemDto
{
	public int Id { get; init; }
	public int ProductId { get; init; }
	public decimal Price { get; init; }
	public int Quantity { get; init; }
}