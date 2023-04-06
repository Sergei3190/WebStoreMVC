using Microsoft.AspNetCore.Mvc;

namespace WebStoreMVC.Controllers;
public class CartController : Controller
{
    public IActionResult Index() => View();
}