using WebStoreMVC.Domain.Entities;
using WebStoreMVC.ViewModels;

namespace WebStoreMVCInfrastructure.Mappings;

public static class ProductMapper
{
    public static ProductViewModel? ToView(this Product? product) => product is null
        ? null
        : new ProductViewModel()
        {
            Id = product.Id,
            Name = product.Name,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            Section = product.Section.Name,
            Brand = product.Brand?.Name
        };

    public static IEnumerable<ProductViewModel?> ToView(this IEnumerable<Product?> products) => products is null
        ? Enumerable.Empty<ProductViewModel?>()
        : products.Select(ToView);
}
