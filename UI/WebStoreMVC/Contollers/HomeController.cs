using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Contollers;

public class HomeController : Controller
{
	public IActionResult Index([FromServices] IProductsService service, [FromServices] IMapper mapper)
	{
		var products = service.GetProducts(new() { PageNumber = 1, PageSize = 6 });

		ViewBag.Products = products.Items
			.OrderBy(p => p.Order)
			.Select(p => mapper.Map<ProductViewModel>(p));

		return View();
	}
}