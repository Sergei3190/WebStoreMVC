using System.Text.Json;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Infrastructure.Mappers;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.Interfaces.Services.Applied;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Services.InCookies;
public class InCookiesCartService : ICartService
{
	private readonly IHttpContextAccessor _httpContext;
	private readonly IProductsService _productsService;
	private readonly string _cartName;

	private Cart Cart
	{
		get
		{
			var context = _httpContext.HttpContext!;

			var cookie = context.Response.Cookies;

			var cart_cookie = context.Request.Cookies[_cartName];

			if (cart_cookie is null)
			{
				var cart = new Cart();
				cookie.Append(_cartName, JsonSerializer.Serialize(cart));
				return cart;
			}

			ReplaceCart(cookie, cart_cookie);
			return JsonSerializer.Deserialize<Cart>(cart_cookie)!;
		}
		set => ReplaceCart(_httpContext.HttpContext!.Response.Cookies, JsonSerializer.Serialize(value));
	}

	private readonly ILogger<InCookiesCartService> _logger;

	public InCookiesCartService(IHttpContextAccessor httpContext,
		IProductsService productsService,
		ILogger<InCookiesCartService> logger)
	{
		_httpContext = httpContext;
		_productsService = productsService;
		_logger = logger;

		var user = _httpContext.HttpContext!.User;
		var userName = user.Identity!.IsAuthenticated ? $"-{user.Identity.Name}" : null;

		_cartName = $"WebStoreMVC.GB.Cart{userName}";
	}

	public void Add(int productId)
	{
		var cart = Cart;
		cart.Add(productId);
		Cart = cart;
	}

	public void Decrement(int productId)
	{
		var cart = Cart;
		cart.Decrement(productId);
		Cart = cart;
	}

	public void Remove(int productId)
	{
		var cart = Cart;
		cart.Remove(productId);
		Cart = cart;
	}

	public void Clear()
	{
		var cart = Cart;
		cart.Clear();
		Cart = cart;
	}

	public CartViewModel GetCartViewModel()
	{
		var cart = Cart;

		var product = _productsService.GetProducts(new ProductFilter() { Ids = cart.Items.Select(i => i.ProductId).ToArray() });

		var products_views = product.ToView().ToDictionary(p => p!.Id);

		return new CartViewModel()
		{
			Items = cart.Items
				.Where(i => products_views.ContainsKey(i.ProductId))
				.Select(i => (products_views[i.ProductId], i.Quantity))!,
		};
	}

	private void ReplaceCart(IResponseCookies cookie, string cart)
	{
		cookie.Delete(_cartName);
		cookie.Append(_cartName, cart);
	}
}
