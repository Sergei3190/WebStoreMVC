using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Services.Interfaces;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Contollers;

public class HomeController : Controller
{
    public IActionResult Index([FromServices] IProductsService service)
    {
        var products = service.GetProducts();

        ViewBag.Products = products
            .OrderBy(p => p.Order)
            .Take(6)
            .Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Price = p.Price
            });

        return View();
    }
}