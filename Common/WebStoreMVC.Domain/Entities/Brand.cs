using Microsoft.EntityFrameworkCore;

using WebStoreMVC.Domain.Entities.Base;
using WebStoreMVC.Domain.Entities.Base.Interfaces;

namespace WebStoreMVC.Domain.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Brand : NamedEntity, IOrderedEntity
{
    public Brand()
    {
        Products = new HashSet<Product>();
    }

    public int Order { get; set; }

    public ICollection<Product> Products { get; set; }
}