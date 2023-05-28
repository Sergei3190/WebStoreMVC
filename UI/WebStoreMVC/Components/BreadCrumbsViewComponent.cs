using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Components;

public class BreadCrumbsViewComponent : ViewComponent
{
	private readonly IProductsService _productsService;

	public BreadCrumbsViewComponent(IProductsService productsService) => _productsService = productsService;

	public IViewComponentResult Invoke()
	{
		var model = new BreadCrumbsViewModel();

		if (int.TryParse(Request.Query["SectionId"], out var section_id))
		{
			model.Section = _productsService.GetSectionById(section_id);

			if (model.Section is { ParentId: { } parent_section_id, Parent: null })
				model.Section.Parent = _productsService.GetSectionById(parent_section_id)!;
		}

		if (int.TryParse(Request.Query["BrandId"], out var brand_id))
			model.Brand = _productsService.GetBrandById(brand_id);

		if (int.TryParse(Request.RouteValues["id"]?.ToString(), out var product_id))
			model.Product = _productsService.GetProductById(product_id).Result?.Name;

		return View(model);
	}
}