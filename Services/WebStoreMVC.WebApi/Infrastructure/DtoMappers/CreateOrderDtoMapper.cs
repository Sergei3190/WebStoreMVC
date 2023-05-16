using WebStoreMVC.Dto;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.WebApi.Infrastructure.DtoMappers;

public static class CreateOrderDtoMapper
{
	public static CartViewModel ToCartViewModel(this IEnumerable<OrderItemDto>? orderItems) => new()
	{
		Items = orderItems?.Select(c => (new ProductViewModel() { Id = c.ProductId }, c.Quantity))!
	};
}
