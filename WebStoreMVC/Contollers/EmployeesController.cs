using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Domain.Identity;
using WebStoreMVC.Services.Interfaces;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Contollers;

[Authorize]
public class EmployeesController : Controller
{
    private readonly IEmployeesService _service;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeesService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public IActionResult Index(int? page, int pageSize = 15)
    {
        IEnumerable<Employee> employees = null;

        if (page is { } _page && pageSize > 0)
            employees = _service.Get(_page * pageSize, pageSize);
        else
            employees = _service.GetAll();

        ViewBag.PagesCount = pageSize > 0 ? (int?)Math.Ceiling(_service.GetCount() / (double)pageSize) : null!;

        var viewModel = employees.Select(e => _mapper.Map<EmployeeViewModel>(e));

        return View(viewModel);
    }

	public async Task<IActionResult> Details(int id)
    {
        var employee = await _service.GetByIdAsync(id);

        if (employee is null)
            return NotFound();

        var viewModel = _mapper.Map<EmployeeViewModel>(employee);

        return View(viewModel);
    }

	[Authorize(Roles = Role.Administrations)]
	public IActionResult Create() => View("Edit", new EmployeeViewModel());

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
            return View(new EmployeeViewModel());

        var employee = await _service.GetByIdAsync(id.Value);

        if (employee is null)
            return NotFound();

        var viewModel = _mapper.Map<EmployeeViewModel>(employee);

        return View(viewModel);
    }

    [HttpPost]
	[Authorize(Roles = Role.Administrations)]
	public async Task<IActionResult> Edit(EmployeeViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        if (!ModelState.IsValid)
            return View(viewModel);

        var employee = _mapper.Map<Employee>(viewModel);

        if (viewModel.Id == 0)
        {
            var employeeId = await _service.AddAsync(employee);
            return RedirectToAction(nameof(Details), new { Id = employeeId });
        }

        await _service.EditAsync(employee);

        return RedirectToAction(nameof(Index));
    }

	[Authorize(Roles = Role.Administrations)]
	public async Task<IActionResult> Delete(int id)
    {
        var employee = await _service.GetByIdAsync(id);

        if (employee is null)
            return NotFound();

        var viewModel = _mapper.Map<EmployeeViewModel>(employee);

        return View(viewModel);
    }

    [HttpPost]
	[Authorize(Roles = Role.Administrations)]
	public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!await _service.DeleteAsync(id))
            return NotFound();

        return RedirectToAction(nameof(Index));
    }
}