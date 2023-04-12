using System.ComponentModel.DataAnnotations;

using WebStoreMVC.Domain.Entities.Base.Interfaces;

namespace WebStoreMVC.Domain.Entities.Base;

public abstract class NamedEntity : Entity, INamedEntity
{
    public NamedEntity()
    {
       Name = null!;
    }

    [Required]
    public string Name { get; set; }
}