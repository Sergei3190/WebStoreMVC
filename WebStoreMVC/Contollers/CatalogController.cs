using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers;
public class CatalogController : Controller
{
    public IActionResult Index() => View();
}