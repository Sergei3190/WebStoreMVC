using System.Diagnostics.CodeAnalysis;

using WebStoreMVC.Domain.Entities.Orders;
using WebStoreMVC.Dto;

namespace WebStoreMVC.WebApi.Infrastructure.DtoMappers;

public static class OrderItemDtoMapper
{
	[return: NotNullIfNotNull("orderItem")]
	public static OrderItemDto? ToDto(this OrderItem? orderItem) => orderItem is null
		? null
		: new OrderItemDto
		{
			Id = orderItem.Id,
			ProductId = orderItem.Product.Id,
			Price = orderItem.Price,
			Quantity = orderItem.Quantity,
		};

	public static IEnumerable<OrderItemDto> ToDto(this IEnumerable<OrderItem>? orderItems) => orderItems?.Select(ToDto)!;
}
