using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Services.Interfaces;

namespace WebStoreMVC.Controllers;
public class CartController : Controller
{

    private readonly ICartService _service;

    public CartController(ICartService service)
    {
        _service = service;
    }
    public IActionResult Index() => View(_service.GetCartViewModel());

    public IActionResult Add(int id)
    {
        _service.Add(id);
        return RedirectToAction(nameof(Index), "Cart");
    }

    public IActionResult Decrement(int id)
    {
        _service.Decrement(id);
        return RedirectToAction(nameof(Index), "Cart");
    }

    public IActionResult Remove(int id)
    {
        _service.Remove(id);
        return RedirectToAction(nameof(Index), "Cart");
    }
}