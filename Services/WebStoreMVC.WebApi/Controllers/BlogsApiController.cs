using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.WebApi.Infrastructure.DtoMappers;

namespace WebStoreMVC.WebApi.Controllers;

[ApiController]
[Route("api/blogs")]
public class BlogsApiController : ControllerBase
{
	private readonly IBlogsService _service;
	private readonly ILogger<BlogsApiController> _logger;

	public BlogsApiController(IBlogsService service, ILogger<BlogsApiController> logger)
	{
		_service = service;
		_logger = logger;
	}

	[HttpGet("{isMain}")]
	public IActionResult GetAll(bool isMain) => Ok(_service.GetAll(isMain).ToDto());

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(int id) => await _service.GetByIdAsync(id) is { } blog
		? Ok(blog.ToDto())
		: NotFound(new { id });
}
