using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Dto;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.WebApi.Infrastructure.DtoMappers;

namespace WebStoreMVC.WebApi.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersApiController : ControllerBase
{
	private readonly IOrderService _service;
	private readonly ILogger<OrdersApiController> _logger;

	public OrdersApiController(IOrderService service, ILogger<OrdersApiController> logger)
	{
		_service = service;
		_logger = logger;
	}

	[HttpGet("user/{userName}")]
	public async Task<IActionResult> GetUserOrders(string userName)
	{
		var result = await _service.GetUserOrdersAsync(userName);

		if (result.Any())
			return Ok(result.ToDto());

		return NoContent();
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetOrderById(int id)
	{
		var result = await _service.GetOrderByIdAsync(id);

		if (result is null)
			return NotFound(new { id });

		return Ok(result.ToDto());
	}

	[HttpPost("{userName}")]
	public async Task<IActionResult> Greate(string userName, [FromBody] CreateOrderDto createOrderDto)
	{
		var cart = createOrderDto.Items.ToCartViewModel();

		var result = await _service.CreateOrderAsync(userName, cart, createOrderDto.Order);

		return CreatedAtAction(nameof(GetOrderById), new { result.Id }, result.ToDto());
	}
}
