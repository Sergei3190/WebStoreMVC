using WebStoreMVC.Domain.Entities;

namespace WebStoreMVC.Interfaces.Services;
public interface IEmployeesService
{
    IEnumerable<Employee> GetAll();

    IEnumerable<Employee> Get(int skip, int take);

    int GetCount();

    Task<Employee?> GetByIdAsync(int id);

    Task<int> AddAsync(Employee employee);

    Task<bool> EditAsync(Employee employee);

    Task<bool> DeleteAsync(int id);
}