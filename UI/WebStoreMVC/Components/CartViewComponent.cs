using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Interfaces.Services.Applied;

namespace WebStoreMVC.Components;
public class CartViewComponent : ViewComponent
{
	private readonly ICartStore _CartStore;

	public CartViewComponent(ICartStore CartStore) => _CartStore = CartStore;

	public IViewComponentResult Invoke()
	{
		ViewBag.Count = _CartStore.Cart.ItemsCount;
		return View();
	}
}
