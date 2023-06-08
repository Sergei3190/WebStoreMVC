using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStoreMVC.Domain;
using WebStoreMVC.Dto;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.WebApi.Infrastructure.DtoMappers;

namespace WebStoreMVC.WebApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsApiController : ControllerBase
{
	private readonly IProductsService _service;
	private readonly ILogger<ProductsApiController> _logger;

	public ProductsApiController(IProductsService service, ILogger<ProductsApiController> logger)
	{
		_service = service;
		_logger = logger;
	}

	[HttpGet("sections")]
	public IActionResult GetSections() => Ok(_service.GetSections().ToDto());

	[HttpGet("sections/{id:int}")]
	public IActionResult GetSectionById(int id) => _service.GetSectionById(id) is { } section
		? Ok(section.ToDto())
		: NotFound(new { id });

	[HttpGet("brands")]
	public IActionResult GetBrands() => Ok(_service.GetBrands().ToDto());

	[HttpGet("brands/{id:int}")]
	public IActionResult GetBrandById(int id) => _service.GetBrandById(id) is { } brand
		? Ok(brand.ToDto())
		: NotFound(new { id });

	[HttpPost("list")]
	public IActionResult GetProducts([FromBody] ProductFilter filter)
	{
		var result = _service.GetProducts(filter);

		if (result.Items.Count() > 0)
			return Ok(result.ToDto());

		return NoContent();
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetProductById(int id) => await _service.GetProductById(id) is { } product
		? Ok(product.ToDto())
		: NotFound(new { id });


	[HttpPost]
	public async Task<IActionResult> Add([FromBody] ProductDto productDto)
	{
		var product = productDto.FromDto();
		var id = await _service.AddAsync(product);
		return CreatedAtAction(nameof(GetProductById), new { id }, product.ToDto());
	}

	[HttpPut]
	public async Task<IActionResult> Edit([FromBody] ProductDto productDto)
	{
		var product = productDto.FromDto();
		var result = await _service.EditAsync(product);
		return result ? Ok(result) : NotFound();
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var result = await _service.DeleteAsync(id);
		return result ? Ok(result) : NotFound();
	}

	[HttpGet("select-sections")]
	public async Task<IActionResult> PopulateSectionDropDownList(CancellationToken cancel = default)
	{
		var result = await _service.PopulateSectionDropDownList();
		return result is { } selections ? Ok(selections) : NoContent();
	}

	[HttpGet("select-brands")]
	public async Task<IActionResult> PopulateBrandDropDownList(CancellationToken cancel = default)
	{
	    var result = await _service.PopulateBrandDropDownList();
		return result is { } brands? Ok(brands) : NoContent();
	}
}
