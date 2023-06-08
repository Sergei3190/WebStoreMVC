using WebStoreMVC.Dto;
using WebStoreMVC.Interfaces;
using WebStoreMVC.Interfaces.Services.Applied;
using WebStoreMVC.ViewModels;
using WebStoreMVC.WebApi.Clients.Base;
using WebStoreMVC.WebApi.Clients.Infrastructure.DtoMappers;

namespace WebStoreMVC.WebApi.Clients.Applied.InCookies;

public class InCookiesCartClient : BaseClient, ICartService
{
	public InCookiesCartClient(HttpClient httpClient)
		: base(httpClient, WebApiAddresses.V1.Applied.InCookies.Cart)
	{
	}

	public void Add(int productId)
	{
		var response = Post<string>($"{Address}?productId={productId}", null);
		response.EnsureSuccessStatusCode();
	}

	public void Decrement(int productId)
	{
		var response = Put<string>($"{Address}/decrement?productId={productId}", null);
		response.EnsureSuccessStatusCode();
	}

	public void Remove(int productId)
	{
		var response = Delete($"{Address}/{productId}");
		response.EnsureSuccessStatusCode();
	}

	public void Clear()
	{
		var response = Post<string>($"{Address}/clear", null);
		response.EnsureSuccessStatusCode();
	}

	public CartViewModel GetCartViewModel()
	{
		var result = Get<IEnumerable<OrderItemDto>>($"{Address}/view-model");
		return result.ToCartViewModel() ?? new CartViewModel();
	}

	public int GetItemsCount()
	{
		var result = Get<int>($"{Address}/items-count");
		return result;
	}
}
