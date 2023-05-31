using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Interfaces.Services.Applied;

namespace WebStoreMVC.Contollers.Api;

public class CartApiController : Controller
{
	private readonly ICartService _service;

	public CartApiController(ICartService service)
	{
		_service = service;
	}

	public IActionResult GetCartView() => ViewComponent("Cart");

	public IActionResult AddApi(int id)
	{
		_service.Add(id);
		return Json(new { id, Message = $"Товар {id} был добавлен в корзину" });
	}

	public IActionResult DecrementApi(int id)
	{
		_service.Decrement(id);
		return Ok(new { id, Message = $"Количество товара в корзине {id} было уменьшено на 1" });
	}

	public IActionResult RemoveApi(int id)
	{
		_service.Remove(id);
		return Ok(new { id, Message = $"Товар {id} был удалён из корзины" });
	}
}