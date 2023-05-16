using System.Diagnostics.CodeAnalysis;

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
		};

	public static IEnumerable<SectionDto> ToDto(this IEnumerable<Section>? sections) => sections?.Select(ToDto)!;
}
