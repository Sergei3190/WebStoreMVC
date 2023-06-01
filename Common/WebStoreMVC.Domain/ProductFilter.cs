namespace WebStoreMVC.Domain;

public class ProductFilter
{
    public int? SectionId { get; set; }

    public int? BrandId { get; set; }

    public int[]? Ids { get; set; }

	public int PageNumber { get; set; } = 1;

	public int? PageSize { get; set; }
}