using System.Diagnostics.CodeAnalysis;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Dto;

namespace WebStoreMVC.WebApi.Infrastructure.DtoMappers;

public static class BrandDtoMapper
{
	[return: NotNullIfNotNull("brand")]
	public static BrandDto? ToDto(this Brand? brand) => brand is null
		? null
		: new BrandDto
		{
			Id = brand.Id,
			Name = brand.Name,
			Order = brand.Order,
			ProductsCount = brand.Products is null ? 0 : brand.Products.Count,
		};

	[return: NotNullIfNotNull("brand")]
	public static Brand? FromDto(this BrandDto? brand) => brand is null
		? null
		: new Brand
		{
			Id = brand.Id,
			Name = brand.Name,
			Order = brand.Order,
			Products = new Product[brand.ProductsCount]
		};

	public static IEnumerable<BrandDto> ToDto(this IEnumerable<Brand>? brands) => brands?.Select(ToDto)!;

	public static IEnumerable<Brand> FromDto(this IEnumerable<BrandDto>? brands) => brands?.Select(FromDto)!;
}
