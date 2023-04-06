namespace WebStoreMVC.ViewModels;

public class ProductViewModel : BaseViewModel
{
    public ProductViewModel()
    {
        ImageUrl = null!;
    }

    public string ImageUrl { get; set; }

    public decimal Price { get; set; }
}