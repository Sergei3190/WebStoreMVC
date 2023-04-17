using Microsoft.EntityFrameworkCore;

using WebStoreMVC.DAL.Context;
using WebStoreMVC.Data;
using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Services.Interfaces;

namespace WebStoreMVC.Services.InSql;

public class InSqlEmployeesService : IEmployeesService
{
    private readonly ILogger<InSqlEmployeesService> _logger;
    private readonly WebStoreMVC_DB _db;

    public InSqlEmployeesService(ILogger<InSqlEmployeesService> logger, WebStoreMVC_DB db)
    {
        _logger = logger;
        _db = db;
    }

    public IEnumerable<Employee> GetAll() => _db.Employees;

    public async Task<Employee?> GetByIdAsync(int id) => await _db.Employees.FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);

    public async Task<int> AddAsync(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        await _db.Employees.AddAsync(employee).ConfigureAwait(false);

        await _db.SaveChangesAsync().ConfigureAwait(false);

        _logger.LogInformation("Добавлен сотрудник {0}", employee);

        return employee.Id;
    }

    public async Task<bool> EditAsync(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        var _employee = await GetByIdAsync(employee.Id).ConfigureAwait(false);
        if (_employee is null)
        {
            _logger.LogWarning("При изменении сотрудника {0} - запись не найдена", employee);
            return false;
        }

        _employee.Age = employee.Age;
        _employee.LastName = employee.LastName;
        _employee.FirstName = employee.FirstName;
        _employee.MiddleName = employee.MiddleName;

        await _db.SaveChangesAsync().ConfigureAwait(false);

        _logger.LogInformation("Изменен сотрудник {0}", employee);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _db.Employees
            .Select(e => new Employee() { Id = e.Id })
            .FirstOrDefaultAsync(e => e.Id == id)
            .ConfigureAwait(false);

        if (employee is null)
        {
            _logger.LogWarning("При удалении сотрудника с id = {0} - запись не найдена", id);
            return false;
        }

         _db.Employees.Remove(employee);

        await _db.SaveChangesAsync().ConfigureAwait(false);

        _logger.LogInformation("Удален сотрудник {0}", employee);

        return true;
    }
}