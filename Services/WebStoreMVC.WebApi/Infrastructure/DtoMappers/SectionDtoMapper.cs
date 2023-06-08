using System.Diagnostics.CodeAnalysis;
using System.Drawing.Drawing2D;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Dto;

namespace WebStoreMVC.WebApi.Infrastructure.DtoMappers;

public static class SectionDtoMapper
{
	[return: NotNullIfNotNull("section")]
	public static SectionDto? ToDto(this Section? section) => section is null
		? null
		: new SectionDto
		{
			Id = section.Id,
			Name = section.Name,
			Order = section.Order,
			ParentId = section.ParentId,
			ProductsCount = section.Products is null ? 0 : section.Products.Count,
		};

	[return: NotNullIfNotNull("section")]
	public static Section? FromDto(this SectionDto? section) => section is null
	? null
	: new Section
	{
		Id = section.Id,
		Name = section.Name,
		Order = section.Order,
		ParentId = section.ParentId,
		Products = new Product[section.ProductsCount]
	};

	public static IEnumerable<SectionDto> ToDto(this IEnumerable<Section>? sections) => sections?.Select(ToDto)!;

	public static IEnumerable<Section> FromDto(this IEnumerable<SectionDto>? sections) => sections?.Select(FromDto)!;
}
