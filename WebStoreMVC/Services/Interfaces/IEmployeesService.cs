using WebStoreMVC.Models;

namespace WebStoreMVC.Services.Interfaces;
public interface IEmployeesService
{
    IEnumerable<Employee> GetAll();

    Employee? GetById(int id);

    int Add(Employee employee);

    bool Edit(Employee employee);

    bool Delete(int id);
}