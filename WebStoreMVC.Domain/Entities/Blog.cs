using WebStoreMVC.Domain.Entities.Base;
using WebStoreMVC.Domain.Entities.Base.Interfaces;

namespace WebStoreMVC.Domain.Entities;

public class Blog : NamedEntity, IOrderedEntity, IImagedEntity
{
    public Blog()
    {
        ShortText = null!;
        FullText = null!;   
    }

    public int Order { get; set; }

    public string? ImageUrl { get; set; }

    public string ShortText { get; set; }

    public string FullText { get; set; }
}