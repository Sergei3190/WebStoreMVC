using System.Net.Http.Json;

using WebStoreMVC.Domain.Entities.Orders;
using WebStoreMVC.Dto;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.ViewModels;
using WebStoreMVC.WebApi.Clients.Base;
using WebStoreMVC.WebApi.Clients.Infrastructure.DtoMappers;

namespace WebStoreMVC.WebApi.Clients.Orders;

public class OrdersClient : BaseClient, IOrderService
{
	public OrdersClient(HttpClient httpClient)
		: base(httpClient, "api/orders")
	{
	}

	public async Task<Order> CreateOrderAsync(string userName, CartViewModel cart, OrderViewModel order, CancellationToken cancel = default)
	{
		var dto = new CreateOrderDto() { Items = cart?.ToDto()!, Order = order };

		var response = await PostAsync($"{Address}/{userName}", dto, cancel).ConfigureAwait(false);

		var result = await response
			.EnsureSuccessStatusCode()
			.Content
			.ReadFromJsonAsync<OrderDto>(cancellationToken: cancel)
			.ConfigureAwait(false);

		if (result is null)
			throw new InvalidOperationException("Не удалось добавить заказ");

		return result.FromDto();
	}

	public async Task<Order?> GetOrderByIdAsync(int id, CancellationToken cancel = default)
	{
		var result = await GetAsync<OrderDto>($"{Address}/{id}", cancel).ConfigureAwait(false);
		return result?.FromDto();
	}

	public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userName, CancellationToken cancel = default)
	{
		var result = await GetAsync<IEnumerable<OrderDto>>($"{Address}/user/{userName}", cancel).ConfigureAwait(false);
		return result?.FromDto() ?? Enumerable.Empty<Order>();
	}
}