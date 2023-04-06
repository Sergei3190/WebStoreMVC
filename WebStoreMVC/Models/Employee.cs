namespace WebStoreMVC.Models;

public class Employee
{
    public Employee()
    {
        LastName = null!;
        FirstName = null!;  
    }

    public int Id { get; set; }
    public int Age { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }

    public override string ToString() => $"(Id: {Id}) {LastName} {FirstName} {MiddleName} - age: {Age}";
}
