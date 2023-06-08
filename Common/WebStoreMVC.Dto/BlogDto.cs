using WebStoreMVC.Dto.Base;
using WebStoreMVC.Dto.Base.Interfaces;

namespace WebStoreMVC.Dto;

public class BlogDto : NamedDto, IImagedDto
{
    public BlogDto()
    {
        ShortText = null!;
        FullText = null!;
    }

    public string? ImageUrl { get; set; }

    public bool IsMain { get; set; }

    public string ShortText { get; set; }

    public string FullText { get; set; }
}