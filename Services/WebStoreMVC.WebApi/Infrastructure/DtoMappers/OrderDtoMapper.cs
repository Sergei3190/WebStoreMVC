using System.Diagnostics.CodeAnalysis;

using WebStoreMVC.Domain.Entities.Orders;
using WebStoreMVC.Dto;

namespace WebStoreMVC.WebApi.Infrastructure.DtoMappers;

public static class OrderDtoMapper
{
	[return: NotNullIfNotNull("order")]
	public static OrderDto? ToDto(this Order? order) => order is null
		? null
		: new OrderDto
		{
			Id = order.Id,
			Phone = order.Phone,
			Address = order.Address,
			Description = order.Description,
			Date = order.Date,
			Items = order.Items.ToDto()
		};

	public static IEnumerable<OrderDto> ToDto(this IEnumerable<Order>? orders) => orders?.Select(ToDto)!;
}
