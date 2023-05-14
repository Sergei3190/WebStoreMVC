using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStoreMVC.Domain.Entities.Identity;

namespace WebStoreMVC.Areas.Admin.Controllers;

[Authorize(Roles = Role.Administrations)]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
