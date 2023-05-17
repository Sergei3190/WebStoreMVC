using System.Diagnostics.CodeAnalysis;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Dto;

namespace WebStoreMVC.WebApi.Clients.Infrastructure.DtoMappers;

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
		};

	public static IEnumerable<SectionDto> ToDto(this IEnumerable<Section>? sections) => sections?.Select(ToDto)!;

	public static IEnumerable<Section> FromDto(this IEnumerable<SectionDto>? sections) => sections?.Select(FromDto)!;
}
