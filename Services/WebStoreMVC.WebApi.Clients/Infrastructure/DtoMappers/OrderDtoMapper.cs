using System.Diagnostics.CodeAnalysis;

using WebStoreMVC.Domain.Entities.Identity;
using WebStoreMVC.Domain.Entities.Orders;
using WebStoreMVC.Dto;

namespace WebStoreMVC.WebApi.Clients.Infrastrucure.DtoMappers;

public static class OrderDtoMapper
{
	[return: NotNullIfNotNull("order")]
	public static Order? FromDto(this OrderDto? order) => order is null
		? null
		: new Order
		{
			Id = order.Id,
			Phone = order.Phone,
			Address = order.Address,
			Description = order.Description,
			Date = order.Date,
			Items = order.Items.FromDto().ToArray(),
		};

	public static IEnumerable<Order> FromDto(this IEnumerable<OrderDto>? orders) => orders?.Select(FromDto)!;
}
