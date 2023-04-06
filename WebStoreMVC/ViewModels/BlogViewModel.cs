using WebStoreMVC.ViewModels.Base;
using WebStoreMVC.ViewModels.Base.Interfaces;

namespace WebStoreMVC.ViewModels;

public class BlogViewModel : NamedViewModel, IImagedViewModel
{
    public BlogViewModel()
    {
        ShortText = null!;
        FullText = null!;
    }

    public string? ImageUrl { get; set; }

    public string ShortText { get; set; }

    public string FullText { get; set; }
}