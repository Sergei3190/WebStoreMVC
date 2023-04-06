using WebStoreMVC.Domain.Entities.Base;
using WebStoreMVC.Domain.Entities.Base.Interfaces;

namespace WebStoreMVC.Domain.Entities;

public class Section : NamedEntity, IOrderedEntity
{
    public int Order { get; set; }

    public int? ParentId { get; set; }
}