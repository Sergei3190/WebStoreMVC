using System.Diagnostics.CodeAnalysis;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Dto;

namespace WebStoreMVC.WebApi.Clients.Infrastrucure.DtoMappers;

public static class ProductDtoMapper
{
	[return: NotNullIfNotNull("product")]
	public static Product? FromDto(this ProductDto? product) => product is null
		? null
		: new Product
		{
			Id = product.Id,
			Name = product.Name,
			Order = product.Order,
			ImageUrl = product.ImageUrl,
			Price = product.Price,
			Section = product.Section.FromDto(),
			Brand = product.Brand?.FromDto(),
		};

	[return: NotNullIfNotNull("product")]
	public static ProductDto? ToDto(this Product? product) => product is null
	? null
	: new ProductDto
	{
		Id = product.Id,
		Name = product.Name,
		Order = product.Order,
		ImageUrl = product.ImageUrl is { } imageUrl ? imageUrl : null!,
		Price = product.Price,
		Section = new SectionDto() { Id = product.SectionId },
		Brand = product.BrandId is { } brandId ? new BrandDto() { Id = brandId } : null,
	};

	public static IEnumerable<Product> FromDto(this IEnumerable<ProductDto>? products) => products?.Select(FromDto)!;

}
