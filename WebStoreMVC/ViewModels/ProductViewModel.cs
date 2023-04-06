using WebStoreMVC.ViewModels.Base;
using WebStoreMVC.ViewModels.Base.Interfaces;

namespace WebStoreMVC.ViewModels;

public class ProductViewModel : NamedViewModel, IImagedViewModel
{
    public ProductViewModel()
    {
        ImageUrl = null!;
    }

    public string? ImageUrl { get; set; }

    public decimal Price { get; set; }
}