﻿using System.Diagnostics.CodeAnalysis;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Dto;
using WebStoreMVC.ViewModels;

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
			SectionId = product.SectionId,
			Section = product.Section?.ToDto(),
			BrandId = product.BrandId,	
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
			SectionId = product.SectionId,
			Section = product.Section?.FromDto()!,
			BrandId = product.BrandId,
			Brand = product.Brand?.FromDto(),
		};

	[return: NotNullIfNotNull("product")]
	public static ProductDto? ToDto(this ProductViewModel? product) => product is null
		? null
		: new ProductDto
		{
			Id = product.Id,
			Name = product.Name,
			Order = product.Order,
			ImageUrl = product.ImageUrl is { } imageUrl ? imageUrl : null!,
			Price = product.Price,
			SectionId = product.SectionId,
			BrandId = product.BrandId,
		};

	[return: NotNullIfNotNull("product")]
	public static ProductViewModel? ToView(this ProductDto? product) => product is null
		? null
		: new ProductViewModel
		{
			Id = product.Id,
			Name = product.Name,
			Order = product.Order,
			ImageUrl = product.ImageUrl is { } imageUrl ? imageUrl : null!,
			Price = product.Price,
			SectionId = product.SectionId,
			BrandId = product.BrandId,
		};

	public static IEnumerable<ProductDto> ToDto(this IEnumerable<Product>? products) => products?.Select(ToDto)!;
}
