using System.Text.Json;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Interfaces.Services.Applied;

namespace WebStoreMVC.Services.InCookies;
public class InCookiesCartStore : ICartStore
{
	private readonly IHttpContextAccessor _httpContext;
	private readonly string _cartName;

	public Cart Cart
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

	private readonly ILogger<InCookiesCartStore> _logger;

	public InCookiesCartStore(IHttpContextAccessor httpContext,
		ILogger<InCookiesCartStore> logger)
	{
		_httpContext = httpContext;
		_logger = logger;

		var user = _httpContext.HttpContext!.User;
		var userName = user.Identity!.IsAuthenticated ? $"-{user.Identity.Name}" : null;

		_cartName = $"WebStoreMVC.GB.Cart{userName}";
	}

	private void ReplaceCart(IResponseCookies cookie, string cart)
	{
		cookie.Delete(_cartName);
		cookie.Append(_cartName, cart);

		_logger.LogInformation("Корзина успешно обновлена. {0}{1}", Environment.NewLine, cart);
	}
}
