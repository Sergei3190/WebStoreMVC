using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Services.Interfaces;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Contollers;

public class HomeController : Controller
{
    public IActionResult Index([FromServices] IProductsService service, [FromServices] IMapper mapper)
    {
        var products = service.GetProducts();

        ViewBag.Products = products
            .OrderBy(p => p.Order)
            .Take(6)
            .Select(p => mapper.Map<ProductViewModel>(p));

        return View();
    }
}