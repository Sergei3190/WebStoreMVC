using WebStoreMVC.Dto;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.WebApi.Clients.Infrastrucure.DtoMappers;

public static class CreateOrderDtoMapper
{
	public static IEnumerable<OrderItemDto>? ToDto(this CartViewModel? cart) => cart?.Items.Select(c => new OrderItemDto()
	{
		ProductId = c.product.Id,
		Price = c.product.Price!,
		Quantity = c.quantity
	});
}
