using WebStoreMVC.Domain.Entities.Base;
using WebStoreMVC.Domain.Entities.Base.Interfaces;

namespace WebStoreMVC.Domain.Entities;

public class Brand : NamedEntity, IOrderedEntity
{
    public int Order { get; set; }
}