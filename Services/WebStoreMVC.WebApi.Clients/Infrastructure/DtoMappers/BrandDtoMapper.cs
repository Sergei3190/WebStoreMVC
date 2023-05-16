using System.Diagnostics.CodeAnalysis;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Dto;

namespace WebStoreMVC.WebApi.Clients.Infrastrucure.DtoMappers;

public static class BrandDtoMapper
{
	[return: NotNullIfNotNull("brand")]
	public static Brand? FromDto(this BrandDto? brand) => brand is null
		? null
		: new Brand
		{
			Id = brand.Id,
			Name = brand.Name,
			Order = brand.Order,
		};

	public static IEnumerable<Brand> FromDto(this IEnumerable<BrandDto>? brands) => brands?.Select(FromDto)!;
}
