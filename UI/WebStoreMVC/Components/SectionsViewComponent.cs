using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Components;

public class SectionsViewComponent : ViewComponent
{
	private readonly IProductsService _service;

	public SectionsViewComponent(IProductsService service) => _service = service;

	public IViewComponentResult Invoke(string sectionId)
	{
		var _sectionId = int.TryParse(sectionId, out var id) ? id : (int?)null;

		return View(new SelectableSectionsViewModel
		{
			Sections = GetSections(_sectionId, out var parentSectionId),
			SectionId = _sectionId,
			ParentSectionId = parentSectionId,
		});
	}

	public IEnumerable<SectionViewModel> GetSections(int? sectionId, out int? parentSectionId)
	{
		parentSectionId = null;

		var sections = _service.GetSections();

		var parentsSections = sections
			.Where(s => s.ParentId is null)
			.OrderBy(s => s.Order)
			.Select(s => new SectionViewModel()
			{
				Id = s.Id,
				Name = s.Name,
				ProductsCount = s.Products.Count,
			})
			.ToArray();

		foreach (var item in parentsSections)
		{
			var children = sections
				.Where(s => s.ParentId == item.Id)
				.OrderBy(s => s.Order);

			var childrensections = new List<SectionViewModel>();

			foreach (var child in children)
			{
				if (child.Id == sectionId)
					parentSectionId = child.ParentId;

				childrensections.Add(new SectionViewModel()
				{
					Id = child.Id,
					Name = child.Name,
					ProductsCount = child.Products.Count,
				});
			}

			item.ChildSections = childrensections;
		}

		return parentsSections;
	}
}