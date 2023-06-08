using Microsoft.Extensions.Logging;

using WebStoreMVC.Domain;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.Interfaces.Services.Applied;
using WebStoreMVC.Services.Mappers;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Services.InCookies;
public class InCookiesCartService : ICartService
{
	private readonly ICartStore _cartStore;
	private readonly IProductsService _productsService;
	private readonly ILogger<InCookiesCartService> _logger;

	public InCookiesCartService(ICartStore cartStore,
		IProductsService productsService,
		ILogger<InCookiesCartService> logger)
	{
		_cartStore = cartStore;
		_productsService = productsService;
		_logger = logger;
	}

	public void Add(int productId)
	{
		var cart = _cartStore.Cart;
		cart.Add(productId);
		_cartStore.Cart = cart;

		_logger.LogInformation("Товар c id = {0} успешно добавлен в корзину. Кол-во заказанных товаров = {1}",
			productId, cart.Items.Sum(i => i.ProductId));
	}

	public void Decrement(int productId)
	{
		var cart = _cartStore.Cart;
		cart.Decrement(productId);
		_cartStore.Cart = cart;

		_logger.LogInformation("Кол-во заказанного товара = {0}", cart.Items
			.Where(i => i.ProductId == productId)
			.Select(i => i.Quantity));
	}

	public void Remove(int productId)
	{
		var cart = _cartStore.Cart;
		cart.Remove(productId);
		_cartStore.Cart = cart;

		_logger.LogInformation("Товар c id = {0} успешно удален из корзины. Кол-во заказанных товаров = {1}",
			productId, cart.Items.Sum(i => i.ProductId));
	}

	public void Clear()
	{
		var cart = _cartStore.Cart;
		cart.Clear();
		_cartStore.Cart = cart;

		_logger.LogInformation("Корзина успешно очищена. Кол-во заказанных товаров = {0}", cart.Items.Sum(i => i.ProductId));
	}

	public CartViewModel GetCartViewModel()
	{
		var cart = _cartStore.Cart;

		var product = _productsService.GetProducts(new ProductFilter() { Ids = cart.Items.Select(i => i.ProductId).ToArray() });

		var products_views = product.Items
			.ToView()
			.ToDictionary(p => p!.Id);

		return new CartViewModel()
		{
			Items = cart.Items
				.Where(i => products_views.ContainsKey(i.ProductId))
				.Select(i => ValueTuple.Create(products_views[i.ProductId], i.Quantity))!,
		};
	}

	public int GetItemsCount() => _cartStore.Cart.ItemsCount;
}
