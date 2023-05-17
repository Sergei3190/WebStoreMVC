using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Dto;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.WebApi.Clients.Base;
using WebStoreMVC.WebApi.Clients.Infrastructure.DtoMappers;

namespace WebStoreMVC.WebApi.Clients.Blogs;

public class BlogsClient : BaseClient, IBlogsService
{
	public BlogsClient(HttpClient httpClient)
		: base(httpClient, "api/blogs")
	{
	}

	public IEnumerable<Blog> GetAll(bool? isMain = false)
	{
		var result = Get<IEnumerable<BlogDto>>($"{Address}/{isMain}");
		return result is { } blogs ? blogs.FromDto() : Enumerable.Empty<Blog>();
	}

	public async Task<Blog?> GetByIdAsync(int id, CancellationToken cansel = default)
	{
		var result = await GetAsync<BlogDto>($"{Address}/{id}", cansel).ConfigureAwait(false);
		return result?.FromDto();
	}
}