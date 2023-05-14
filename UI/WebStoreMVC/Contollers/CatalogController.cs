using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.Services.Mappers.Mappings;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Contollers;
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

    public async Task<IActionResult> Details(int id)
    {
        var product = await _service.GetProductById(id).ConfigureAwait(false);

        if (product is null)
            return NotFound();

        return View(product.ToView());
    }
}