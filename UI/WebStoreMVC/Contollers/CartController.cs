using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.Interfaces.Services.Applied;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Contollers;
public class CartController : Controller
{

    private readonly ICartService _service;

    public CartController(ICartService service)
    {
        _service = service;
    }
    public IActionResult Index() => View(new CartOrderViewModel()
    {
        Cart = _service.GetCartViewModel(),
        Order = new()
    });

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

    [Authorize, HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(OrderViewModel orderViewModel, [FromServices] IOrderService _orderService)
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(Index), new CartOrderViewModel()
            {
                Cart = _service.GetCartViewModel(),
                Order = orderViewModel
            });
        }

        var order = await _orderService.CreateOrderAsync(
            User.Identity!.Name!,
            _service.GetCartViewModel(),
            orderViewModel);

        _service.Clear();

        return RedirectToAction(nameof(OrderConfirmed), new { order.Id });
    }

    public IActionResult OrderConfirmed(int id)
    {
        ViewBag.OrderId = id;
        return View();
    }
}