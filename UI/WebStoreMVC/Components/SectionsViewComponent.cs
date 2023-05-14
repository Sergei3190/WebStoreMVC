using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Components;

public class SectionsViewComponent : ViewComponent
{
    private readonly IProductsService _service;

    public SectionsViewComponent(IProductsService service) => _service = service;

    public IViewComponentResult Invoke()
    {
        var sections = _service.GetSections();

        var parentsSections = sections
            .Where(s => s.ParentId is null)
            .OrderBy(s => s.Order)
            .Select(s => new SectionViewModel()
            {
                Id = s.Id,
                Name = s.Name,
            })
            .ToArray();

        foreach (var item in parentsSections)
        {
            item.ChildSections = sections
                .Where(s => s.ParentId == item.Id)
                .OrderBy(s => s.Order)
                .Select(s => new SectionViewModel()
                {
                    Id = s.Id,
                    Name = s.Name
                });
        }

        return View(parentsSections);
    }
}