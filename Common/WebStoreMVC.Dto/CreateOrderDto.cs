using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Dto;
public class CreateOrderDto
{
    public OrderViewModel Order { get; init; }
	public IEnumerable<OrderItemDto> Items { get; init; }
}