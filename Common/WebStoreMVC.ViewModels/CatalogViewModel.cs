namespace WebStoreMVC.ViewModels;

public class CatalogViewModel
{
    public CatalogViewModel()
    {
        Products = Enumerable.Empty<ProductViewModel>();
    }

    public int? SectionId { get; set; }

    public int? BrandId { get; set; }

    public IEnumerable<ProductViewModel> Products { get; set; }
}