namespace WebStoreMVC.Dto;
public class OrderDto
{
    public int Id { get; init; }
	public string Address { get; init; } = null!;
	public string Phone { get; init; } = null!;
	public string? Description { get; init; }
	public DateTimeOffset Date { get; init; }
	public IEnumerable<OrderItemDto> Items { get; init; }
}