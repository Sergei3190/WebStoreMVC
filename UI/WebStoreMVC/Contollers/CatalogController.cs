using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Domain;
using WebStoreMVC.Infrastructure.Mappers;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Contollers;
public class CatalogController : Controller
{
	private readonly IProductsService _service;
	private readonly IMapper _mapper;
	private readonly IConfiguration _configuration;
	private readonly string _catalogPageSize;

	public CatalogController(IProductsService service, IMapper mapper, IConfiguration configuration)
	{
		_service = service;
		_mapper = mapper;
		_configuration = configuration;
		_catalogPageSize = "CatalogPageSize";
	}

	public IActionResult Index([Bind("SectionId, BrandId, PageNumber, PageSize")] ProductFilter filter)
	{
		filter.PageSize ??= int.TryParse(_configuration[_catalogPageSize], out var pageSize) ? pageSize : null;

		var products = _service.GetProducts(filter);

		return View(new CatalogViewModel()
		{
			SectionId = filter.SectionId,
			BrandId = filter.BrandId,
			Products = products.Items
				.OrderBy(p => p.Order)
				.Select(p => _mapper.Map<ProductViewModel>(p)),
			Page = new PageViewModel()
			{
				Page = products.PageNumber,
				PageSize = products.PageSize,
				TotalPages = products.PageCount
			}
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