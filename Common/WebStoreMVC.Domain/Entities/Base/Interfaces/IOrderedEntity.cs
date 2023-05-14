namespace WebStoreMVC.Domain.Entities.Base.Interfaces;

public interface IOrderedEntity : IEntity
{
    int Order { get; set; }
}