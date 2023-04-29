using WebStoreMVC.ViewModels.Base;
using WebStoreMVC.ViewModels.Base.Interfaces;

namespace WebStoreMVC.ViewModels;

public class ProductViewModel : NamedViewModel, IImagedViewModel
{
    public ProductViewModel()
    {
        ImageUrl = null!;
        Section = null!;
    }

    public string? ImageUrl { get; set; }

    public string Section { get; set; }

    public string? Brand { get; set; }

    public decimal Price { get; set; }
}