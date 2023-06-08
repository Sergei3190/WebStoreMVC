using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Domain;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Contollers.Api;

public class CatalogApiController : Controller
{
	private readonly IProductsService _service;
	private readonly IMapper _mapper;
	private readonly IConfiguration _configuration;
	private readonly string _catalogPageSize;

	public CatalogApiController(IProductsService service, IMapper mapper, IConfiguration configuration)
	{
		_service = service;
		_mapper = mapper;
		_configuration = configuration;
		_catalogPageSize = "CatalogPageSize";
	}

	public IActionResult GetProductsApi([Bind("SectionId,BrandId,PageNumber,PageSize")] ProductFilter filter)
	{
		filter.PageSize ??= _configuration.GetValue(_catalogPageSize, 6);

		var products = _service.GetProducts(filter);

		return PartialView("Partial/_Products", products.Items
			.Select(p => _mapper.Map<ProductViewModel>(p)));
	}
}