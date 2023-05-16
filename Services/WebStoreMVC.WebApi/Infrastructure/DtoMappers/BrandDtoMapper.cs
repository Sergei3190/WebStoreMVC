﻿using System.Diagnostics.CodeAnalysis;

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
		};

	public static IEnumerable<BrandDto> ToDto(this IEnumerable<Brand>? brands) => brands?.Select(ToDto)!;
}