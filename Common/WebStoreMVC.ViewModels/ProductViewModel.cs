using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Http;

using WebStoreMVC.ViewModels.Base;
using WebStoreMVC.ViewModels.Base.Interfaces;

namespace WebStoreMVC.ViewModels;

public class ProductViewModel : NamedViewModel, IImagedViewModel
{
    public ProductViewModel()
    {
        ImageUrl = null!;
        Section = null!;
        FormFile = null!;   
    }

    public int Order { get; set; }

    [Display(Name = "File")]
    public IFormFile FormFile { get; set; }

    public string? ImageUrl { get; set; }

    public int SectionId { get; set; }
    public SectionViewModel? Section { get; set; }

    public int? BrandId { get; set; }
    public BrandViewModel? Brand { get; set; }

    public decimal Price { get; set; }
}