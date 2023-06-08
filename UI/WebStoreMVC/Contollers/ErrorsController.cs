using Microsoft.AspNetCore.Mvc;

namespace WebStoreMVC.Contollers;

public class ErrorsController : Controller
{
    private readonly string _error404 = "404";

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Error404() => PartialView("Partial/_Error404",_error404);
}