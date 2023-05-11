using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Domain.Entities.Identity;
using WebStoreMVC.Services.Interfaces;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Areas.Admin.Controllers;

[Authorize(Roles = Role.Administrations)]
public class ProductsController : Controller
{
    private readonly IProductsService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductsService service,
        IMapper mapper,
        ILogger<ProductsController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var products = _service.GetProducts()
            .Select(p => _mapper.Map<ProductViewModel>(p));

        return View(products);
    }

    public IActionResult Create()
    {
        ViewBag.SectionId = _service.PopulateSectionDropDownList();
        ViewBag.BrandId = _service.PopulateBrandDropDownList();

        return View("Edit", new ProductViewModel());
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
            return View(new ProductViewModel());
        var product = await _service.GetProductById(id.Value);

        if (product is null)
            return NotFound();

        var viewModel = _mapper.Map<ProductViewModel>(product);

        ViewBag.SectionId = _service.PopulateSectionDropDownList();
        ViewBag.BrandId = _service.PopulateBrandDropDownList();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        if (!ModelState.IsValid)
            return View(viewModel);

        var product = _mapper.Map<Product>(viewModel);

        if (viewModel.Id == 0)
            await _service.AddAsync(product);
        else
            await _service.EditAsync(product);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var product = await _service.GetProductById(id).ConfigureAwait(false);

        if (product is null)
            return NotFound();

        var viewModel = _mapper.Map<ProductViewModel>(product);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!await _service.DeleteAsync(id))
            return NotFound();

        return RedirectToAction(nameof(Index));
    }
}
