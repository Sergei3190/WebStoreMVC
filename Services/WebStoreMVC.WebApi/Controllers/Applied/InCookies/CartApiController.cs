using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Interfaces;
using WebStoreMVC.Interfaces.Services.Applied;
using WebStoreMVC.WebApi.Infrastructure.DtoMappers;

namespace WebStoreMVC.WebApi.Controllers.Applied.InCookies;

[ApiController]
[Route(WebApiAddresses.V1.Applied.InCookies.Cart)]
public class CartApiController : ControllerBase
{
	private readonly ICartService _service;
	private readonly ILogger<CartApiController> _logger;

	public CartApiController(
		ICartService service,
		ILogger<CartApiController> logger)
	{
		_service = service;
		_logger = logger;	
	}

	[HttpGet("view-model")]
	public IActionResult GetCartViewModel()
	{
		var result = _service.GetCartViewModel();

		if (result is null)
			return NotFound();

		return Ok(result.ToDto());
	}

	[HttpGet("items-count")]
	public IActionResult GetItemsCount() => Ok(_service.GetItemsCount());

	[HttpPost]
	public IActionResult Add(int productId)
	{
		_service.Add(productId);
		return Ok();
	}

	[HttpPut("decrement")]
	public IActionResult Decrement(int productId)
	{
		_service.Decrement(productId);
		return Ok();
	}

	[HttpPost("clear")]
	public IActionResult Decrement()
	{
		_service.Clear();
		return Ok();
	}

	[HttpDelete("{productId:int}")]
	public IActionResult Remove(int productId)
	{
		_service.Remove(productId);
		return Ok();
	}
}
