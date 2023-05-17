﻿using System.Diagnostics.CodeAnalysis;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Dto;

namespace WebStoreMVC.WebApi.Infrastructure.DtoMappers;

public static class ProductDtoMapper
{
	[return: NotNullIfNotNull("product")]
	public static ProductDto? ToDto(this Product? product) => product is null
		? null
		: new ProductDto
		{
			Id = product.Id,
			Name = product.Name,
			Order = product.Order,
			ImageUrl = product.ImageUrl!,
			Price = product.Price,
			Section = product.Section.ToDto(),
			Brand = product.Brand?.ToDto(),
		};

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

	public static IEnumerable<ProductDto> ToDto(this IEnumerable<Product>? products) => products?.Select(ToDto)!;
}
