using System.Diagnostics.CodeAnalysis;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Dto;

namespace WebStoreMVC.WebApi.Infrastructure.DtoMappers;

public static class BlogDtoMapper
{
	[return: NotNullIfNotNull("blog")]
	public static BlogDto? ToDto(this Blog? blog) => blog is null
		? null
		: new BlogDto
		{
			Id = blog.Id,
			Name = blog.Name,
			ImageUrl = blog.ImageUrl,
			IsMain = blog.IsMain,
			ShortText = blog.ShortText,
			FullText = blog.FullText,
		};

	[return: NotNullIfNotNull("Blog")]
	public static Blog? FromDto(this BlogDto? blog) => blog is null
		? null
		: new Blog
		{
			Id = blog.Id,
			Name = blog.Name,
			ImageUrl = blog.ImageUrl,
			IsMain = blog.IsMain,
			ShortText = blog.ShortText,
			FullText = blog.FullText,
		};

	public static IEnumerable<BlogDto> ToDto(this IEnumerable<Blog>? blogs) => blogs?.Select(ToDto)!;

	public static IEnumerable<Blog> FromDto(this IEnumerable<BlogDto>? blogs) => blogs?.Select(FromDto)!;
}
