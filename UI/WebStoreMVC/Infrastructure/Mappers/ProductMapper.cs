using System.Diagnostics.CodeAnalysis;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Infrastructure.Mappers;

public static class ProductMapper
{
	[return: NotNullIfNotNull("product")]
	public static ProductViewModel? ToView(this Product? product) => product is null
		? null
		: new ProductViewModel()
		{
			Id = product.Id,
			Name = product.Name,
			ImageUrl = product.ImageUrl,
			Price = product.Price,
			SectionId = product.SectionId,
			Section = new SectionViewModel()
			{
				Id = product.Section.Id,
				Name = product.Section.Name
			},
			BrandId = product.BrandId,
			Brand = product.Brand != null ? new BrandViewModel()
			{
				Id = product.Brand.Id,
				Name = product.Brand.Name
			} : null
		};

	public static IEnumerable<ProductViewModel?> ToView(this IEnumerable<Product?> products) => products is null
		? Enumerable.Empty<ProductViewModel?>()
		: products.Select(ToView);
}
