using WebStoreMVC.DAL.Context;
using WebStoreMVC.Data;
using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Services.Interfaces;

namespace WebStoreMVC.Services.InSql;

//TODO
public class InSqlEmployeesService : IEmployeesService
{
    private readonly ILogger<InSqlEmployeesService> _logger;
    private readonly WebStoreMVC_DB _db;
    private int _lastFreeId;

    public InSqlEmployeesService(ILogger<InSqlEmployeesService> logger, WebStoreMVC_DB db)
    {
        _logger = logger;
        _db = db;
    }

    public IEnumerable<Employee> GetAll() => _db.Employees;

    public Employee? GetById(int id) => _db.Employees.FirstOrDefault(e => e.Id == id);

    public int Add(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        if (_db.Employees.Contains(employee))
            return employee.Id;

        employee.Id = _lastFreeId++;

        _db.Employees.Add(employee);

        _logger.LogInformation("Добавлен сотрудник {0}", employee);

        return employee.Id;
    }

    public bool Edit(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        if (_db.Employees.Contains(employee))
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

         _db.Employees.Remove(employee);

        _logger.LogInformation("Удален сотрудник {0}", employee);

        return true;
    }
}