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

    public IActionResult Index(int? sectionId, int? brandId)
    {
        var filter = new ProductFilter() { SectionId = sectionId, BrandId = brandId };

        var products = _service.GetProducts(filter);

        return View(new CatalogViewModel()
        {
            SectionId = sectionId,
            BrandId = brandId,
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