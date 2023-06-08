using WebStoreMVC.Dto;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.WebApi.Infrastructure.DtoMappers;

public static class CreateOrderDtoMapper
{
	public static CartViewModel ToCartViewModel(this IEnumerable<OrderItemDto>? orderItems) => new()
	{
		Items = orderItems?.Select(c => (c.Product.ToView(), c.Quantity))!
	};

	public static IEnumerable<OrderItemDto>? ToDto(this CartViewModel? cart) => cart?.Items.Select(c => new OrderItemDto()
	{
		Product = c.product.ToDto(),
		Price = c.product.Price!,
		Quantity = c.quantity
	});
}
