namespace WebStoreMVC.ViewModels;

public class CatalogViewModel
{
    public CatalogViewModel()
    {
        Products = Enumerable.Empty<ProductViewModel>();
        Page = null!;
    }

    public int? SectionId { get; set; }

    public int? BrandId { get; set; }

    public IEnumerable<ProductViewModel> Products { get; set; }

	public PageViewModel Page { get; set; }
}