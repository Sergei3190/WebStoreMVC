using System.Diagnostics.CodeAnalysis;

using WebStoreMVC.Domain.Entities.Orders;
using WebStoreMVC.Dto;

namespace WebStoreMVC.WebApi.Clients.Infrastructure.DtoMappers;

public static class OrderItemDtoMapper
{
	[return: NotNullIfNotNull("orderItem")]
	public static OrderItem? FromDto(this OrderItemDto? orderItem) => orderItem is null
		? null
		: new OrderItem
		{
			Id = orderItem.Id,
			Product = orderItem.Product.FromDto(),
			Price = orderItem.Price,
			Quantity = orderItem.Quantity,
		};

	public static IEnumerable<OrderItem> FromDto(this IEnumerable<OrderItemDto>? orderItems) => orderItems?.Select(FromDto)!;
}
