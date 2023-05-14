using Microsoft.AspNetCore.Mvc;

namespace WebStoreMVC.Contollers;

public class ContactsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}