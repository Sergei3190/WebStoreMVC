using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using WebStoreMVC.Domain.Entities.Base;
using WebStoreMVC.Domain.Entities.Base.Interfaces;

namespace WebStoreMVC.Domain.Entities;

[Index(nameof(Name), IsUnique = false)]
public class Product : NamedEntity, IOrderedEntity, IImagedEntity
{
    public Product()
    {
        Section = null!;
        ImageUrl = null!;
    }

    public int Order { get; set; }

    public int SectionId { get; set; }

    [Required]
    [ForeignKey(nameof(SectionId))]
    public Section Section { get; set; }

    public int? BrandId { get; set; }

    [ForeignKey(nameof(BrandId))]
    public Brand? Brand { get; set; }

    public string? ImageUrl { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}