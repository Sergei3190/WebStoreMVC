using Microsoft.Extensions.Logging;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.Services.Data;

namespace WebStoreMVC.Services.InMemory;

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

    public IEnumerable<Employee> Get(int skip, int take)
    {
        if (take == 0 || skip > GetCount())
            return Enumerable.Empty<Employee>();

        IEnumerable<Employee> query = _employees;

        if (skip > 0)
            query.Skip(skip);

        return query.Take(take);
    }

    public int GetCount() => _employees.Count;

    public async Task<Employee?> GetByIdAsync(int id) => await Task.FromResult(_employees.FirstOrDefault(e => e.Id == id)).ConfigureAwait(false);

    public async Task<int> AddAsync(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        if (_employees.Contains(employee))
            return employee.Id;

        employee.Id = _lastFreeId++;

        _employees.Add(employee);

        _logger.LogInformation("Добавлен сотрудник {0}", employee);

        return await Task.FromResult(employee.Id).ConfigureAwait(false);
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

        _logger.LogInformation("Изменен сотрудник {0}", employee);

        return await Task.FromResult(true).ConfigureAwait(false);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await GetByIdAsync(id).ConfigureAwait(false);
        if (employee is null)
        {
            _logger.LogWarning("При удалении сотрудника с id = {0} - запись не найдена", id);
            return false;
        }

        _employees.Remove(employee);

        _logger.LogInformation("Удален сотрудник {0}", employee);

        return await Task.FromResult(true).ConfigureAwait(false);
    }
}