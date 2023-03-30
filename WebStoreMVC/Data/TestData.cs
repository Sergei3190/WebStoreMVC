using WebStoreMVC.Models;

namespace WebStoreMVC.Data;

public static class TestData
{
    public static ICollection<Employee> Employees { get; } = new List<Employee>()
    {
        new () { Id = 1, Age = 23, LastName = "Иванов", FirstName = "Иван", MiddleName = "Иванович" },
        new () { Id = 2, Age = 27, LastName = "Петров", FirstName = "Петр", MiddleName = "Петрович" },
        new () { Id = 3, Age = 18, LastName = "Сидоров", FirstName = "Сидор", MiddleName = "Сидорович" }
    };
}