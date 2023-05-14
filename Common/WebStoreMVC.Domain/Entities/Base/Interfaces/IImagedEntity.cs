namespace WebStoreMVC.Domain.Entities.Base.Interfaces;

public interface IImagedEntity : IEntity
{
    string? ImageUrl { get; set; }
}