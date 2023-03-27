using Microsoft.AspNetCore.Mvc;

namespace WebStoreMVC.Contollers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    //TODO реализовать получение контактов и ошибку, сделать привязку к макету дизайна
    public IActionResult Contacts() => View();
}