using Microsoft.AspNetCore.Mvc;
using WebStoreMVC.Models;

namespace WebStoreMVC.Contollers;

public class EmployeeController : Controller
{
    private static readonly List<Employee> _employees = new List<Employee>()
    {
        new Employee() { Id = 1, Age = 23, LastName = "Иванов", FirstName = "Иван", MiddleName = "Иванович" },
        new Employee() { Id = 2, Age = 27, LastName = "Петров", FirstName = "Петр", MiddleName = "Петрович" },
        new Employee() { Id = 3, Age = 18, LastName = "Сидоров", FirstName = "Сидор", MiddleName = "Сидорович" }
    };

    public IActionResult List()
    {
        return View(_employees);
    }

    public IActionResult Details(int id)
    {
         var employee = _employees.FirstOrDefault(x => x.Id == id);

        if (employee == null)
            return NotFound();

        return View(employee);
    }
}
