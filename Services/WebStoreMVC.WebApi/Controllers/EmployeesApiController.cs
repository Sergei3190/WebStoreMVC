using Microsoft.AspNetCore.Mvc;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.WebApi.Infrastructure.Mappers;

namespace WebStoreMVC.WebApi.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesApiController : ControllerBase
{
    private readonly IEmployeesService _service;
    private readonly ILogger<EmployeesApiController> _logger;

    public EmployeesApiController(IEmployeesService service, ILogger<EmployeesApiController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("count")]
	public IActionResult GetCount()
    {
        var result = _service.GetCount();
        return Ok(result);
    }

	[HttpGet]
	public IActionResult GetAll()
	{
		if (_service.GetCount() == 0)
            return NoContent();

        var result = _service.GetAll(); 
		return Ok(result.ToDto());
	}

    [HttpGet("({skip:int}/{take:int})")]
    [HttpGet("{skip:int}/{take:int}")]
	public IActionResult Get(int skip, int take)
	{
        if (skip < 0 || take < 0)
            return BadRequest();

	    if (take == 0 || skip > _service.GetCount())
            return NoContent();

        var result = _service.Get(skip, take);
        return Ok(result.ToDto());
	}

    [HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);
		return result is null 
            ? NotFound()
            : Ok(result.ToDto());
	}

	[HttpPost]
	public async Task<IActionResult> Add([FromBody] Employee employee)
	{
		var id = await _service.AddAsync(employee);
        return CreatedAtAction(nameof(GetById), new { id }, employee);
	}

	[HttpPut]
	public async Task<IActionResult> Edit([FromBody] Employee employee)
	{
		var result = await _service.EditAsync(employee);
		return result ? Ok(result) : NotFound();
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var result = await _service.DeleteAsync(id);
		return result ? Ok(result) : NotFound();
	}
}
