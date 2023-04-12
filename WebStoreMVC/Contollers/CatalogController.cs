using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Services.Interfaces;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Controllers;
public class CatalogController : Controller
{
    private readonly IProductsService _service;

    public CatalogController(IProductsService service)
    {
        _service = service;
    }

    public IActionResult Index([Bind("SectionId", "BrandId")] ProductFilter filter)
    {
        var products = _service.GetProducts(filter);

        return View(new CatalogViewModel()
        {
            SectionId = filter.SectionId,
            BrandId = filter.BrandId,
            Products = products
                .OrderBy(p => p.Order)
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price
                })
        });
    }
}