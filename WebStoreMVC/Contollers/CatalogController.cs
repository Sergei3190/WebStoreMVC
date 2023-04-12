using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Services.Interfaces;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Controllers;
public class CatalogController : Controller
{
    private readonly IProductsService _service;
    private readonly IMapper _mapper;

    public CatalogController(IProductsService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
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
                .Select(p => _mapper.Map<ProductViewModel>(p))
        });
    }
}