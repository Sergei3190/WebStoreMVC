using WebStoreMVC.Domain.Entities.Base;

namespace WebStoreMVC.Domain.Entities;

public class Employee : Entity
{
    public Employee()
    {
        LastName = null!;
        FirstName = null!;
    }

    public int Age { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }

    public override string ToString() => $"(Id: {Id}) {LastName} {FirstName} {MiddleName} - age: {Age}";
}
