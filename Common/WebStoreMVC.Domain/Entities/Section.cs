using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using WebStoreMVC.Domain.Entities.Base;
using WebStoreMVC.Domain.Entities.Base.Interfaces;

namespace WebStoreMVC.Domain.Entities;

[Index(nameof(Name), IsUnique = false)]
public class Section : NamedEntity, IOrderedEntity
{
    public Section()
    {
        Products = new HashSet<Product>();
    }

    public int Order { get; set; }

    public int? ParentId { get; set; }

    [ForeignKey(nameof(ParentId))]
    public Section? Parent { get; set; }

    public ICollection<Product> Products { get; set; }
}