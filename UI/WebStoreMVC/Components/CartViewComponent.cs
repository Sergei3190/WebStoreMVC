using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Interfaces.Services.Applied;

namespace WebStoreMVC.Components;
public class CartViewComponent : ViewComponent
{
	private readonly ICartService _service;

	public CartViewComponent(ICartService service) => _service = service;

	public IViewComponentResult Invoke()
	{
		ViewBag.Count = _service.GetItemsCount();
		return View();
	}
}
