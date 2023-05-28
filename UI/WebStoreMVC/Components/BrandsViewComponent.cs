using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Components;

public class BrandsViewComponent : ViewComponent
{
    private readonly IProductsService _service;

    public BrandsViewComponent(IProductsService service) => _service = service;

    public IViewComponentResult Invoke(string brandId) 
    {
        ViewBag.BrandId = (int.TryParse(brandId, out var id) ? id : (int?)null)!;
        return View(GetBrands());
	}

    private IEnumerable<BrandViewModel> GetBrands() => _service.GetBrands()
        .OrderBy(b => b.Order)
        .Select(b => new BrandViewModel()
        {
            Id = b.Id,
            Name = b.Name
        });
}