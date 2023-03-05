using Microsoft.AspNetCore.Mvc;

namespace WebStoreMVC.Contollers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}