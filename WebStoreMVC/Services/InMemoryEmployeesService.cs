using WebStoreMVC.Data;
using WebStoreMVC.Models;
using WebStoreMVC.Services.Interfaces;

namespace WebStoreMVC.Services;

public class InMemoryEmployeesService : IEmployeesService
{
    private readonly ICollection<Employee> _employees;
    private readonly ILogger<InMemoryEmployeesService> _logger;
    private int _lastFreeId;

    public InMemoryEmployeesService(ILogger<InMemoryEmployeesService> logger)
    {
        _employees = TestData.Employees;
        _logger = logger;
        _lastFreeId = _employees.Any() ? _employees.Max(e => e.Id) + 1 : 1;
    }

    public IEnumerable<Employee> GetAll() => _employees;

    public Employee? GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);

    public int Add(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        if (_employees.Contains(employee))
            return employee.Id;

        employee.Id = _lastFreeId++;

        _employees.Add(employee);

        _logger.LogInformation("Добавлен сотрудник {0}", employee);

        return employee.Id;
    }

    public bool Edit(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        if (_employees.Contains(employee))
            return true;

        var _employee = GetById(employee.Id);
        if (_employee is null)
        {
            _logger.LogWarning("При изменении сотрудника {0} - запись не найдена", employee);
            return false;
        }

        _employee.Age = employee.Age;
        _employee.LastName = employee.LastName;
        _employee.FirstName = employee.FirstName;
        _employee.MiddleName = employee.MiddleName;

        _logger.LogInformation("Изменен сотрудник {0}", employee);

        return true;
    }

    public bool Delete(int id)
    {
        var employee = GetById(id);
        if (employee is null)
        {
            _logger.LogWarning("При удалении сотрудника с id = {0} - запись не найдена", id);
            return false;
        }

        _employees.Remove(employee);

        _logger.LogInformation("Удален сотрудник {0}", employee);

        return true;
    }
}