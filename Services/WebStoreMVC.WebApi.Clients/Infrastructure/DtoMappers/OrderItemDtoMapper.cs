using System.Diagnostics.CodeAnalysis;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Domain.Entities.Orders;
using WebStoreMVC.Dto;

namespace WebStoreMVC.WebApi.Clients.Infrastrucure.DtoMappers;

public static class OrderItemDtoMapper
{
	[return: NotNullIfNotNull("orderItem")]
	public static OrderItem? FromDto(this OrderItemDto? orderItem) => orderItem is null
		? null
		: new OrderItem
		{
			Id = orderItem.Id,
			Product = new Product() { Id = orderItem.ProductId },
			Price = orderItem.Price,
			Quantity = orderItem.Quantity,
		};

	public static IEnumerable<OrderItem> FromDto(this IEnumerable<OrderItemDto>? orderItems) => orderItems?.Select(FromDto)!;
}
