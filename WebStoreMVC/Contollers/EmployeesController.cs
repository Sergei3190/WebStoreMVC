using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Services.Interfaces;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Contollers;

public class EmployeesController : Controller
{
    private readonly IEmployeesService _service;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeesService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public IActionResult Index() => View(GetAll());

    public IActionResult Details(int id)
    {
        var employee = _service.GetById(id);

        if (employee is null)
            return NotFound();

        var viewModel = _mapper.Map<EmployeeViewModel>(employee);

        return View(viewModel);
    }

    public IActionResult Create() => View("Edit", new EmployeeViewModel());

    public IActionResult Edit(int? id)
    {
        if (id is null)
            return View(new EmployeeViewModel());

        var employee = _service.GetById(id.Value);

        if (employee is null)
            return NotFound();

        var viewModel = _mapper.Map<EmployeeViewModel>(employee);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Edit(EmployeeViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        if (!ModelState.IsValid)
            return View(viewModel);

        var employee = _mapper.Map<Employee>(viewModel);

        if (viewModel.Id == 0)
        {
            var employeeId = _service.Add(employee);
            return RedirectToAction(nameof(Details), new { Id = employeeId });
        }

        _service.Edit(employee);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var employee = _service.GetById(id);

        if (employee is null)
            return NotFound();

        var viewModel = _mapper.Map<EmployeeViewModel>(employee);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!_service.Delete(id))
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    private IEnumerable<EmployeeViewModel> GetAll()
    {
        var employees = _service.GetAll();
        return employees
            .Select(e => _mapper.Map<EmployeeViewModel>(e));
    }
}