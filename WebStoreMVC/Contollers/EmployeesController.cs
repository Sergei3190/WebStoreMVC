using Microsoft.AspNetCore.Mvc;
using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Services.Interfaces;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Contollers;

public class EmployeesController : Controller
{
    private readonly IEmployeesService _service;

    public EmployeesController(IEmployeesService service)
    {
        _service = service;
    }

    public IActionResult Index() => View(GetAll());

    public IActionResult Details(int id)
    {
        var employee = _service.GetById(id);

        if (employee is null)
            return NotFound();

        var viewModel = new EmployeeViewModel()
        {
            Id = id,
            LastName = employee.LastName,
            FirstName = employee.FirstName,
            MiddleName = employee.MiddleName,
            Age = employee.Age,
        };

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

        var viewModel = new EmployeeViewModel()
        {
            Id = id.Value,
            LastName = employee.LastName,
            FirstName = employee.FirstName,
            MiddleName = employee.MiddleName,
            Age = employee.Age,
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Edit(EmployeeViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        if (!ModelState.IsValid)
            return View(viewModel);

        var employee = new Employee()
        {
            Id = viewModel.Id,
            LastName = viewModel.LastName,
            FirstName = viewModel.FirstName,
            MiddleName = viewModel.MiddleName,
            Age = viewModel.Age,
        };

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

        var viewModel = new EmployeeViewModel()
        {
            Id = id,
            LastName = employee.LastName,
            FirstName = employee.FirstName,
            MiddleName = employee.MiddleName,
            Age = employee.Age,
        };

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
            .Select(e => new EmployeeViewModel()
            {
                Id = e.Id,
                LastName = e.LastName,
                FirstName = e.FirstName,
                MiddleName = e.MiddleName,
                Age = e.Age,
            });
    }
}