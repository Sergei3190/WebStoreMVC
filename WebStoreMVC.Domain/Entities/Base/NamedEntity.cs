using WebStoreMVC.Domain.Entities.Base.Interfaces;

namespace WebStoreMVC.Domain.Entities.Base;

public abstract class NamedEntity : Entity, INamedEntity
{
    public string Name { get; set; } = null!;
}