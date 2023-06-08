using Microsoft.EntityFrameworkCore;

using WebStoreMVC.Domain.Entities.Base;

namespace WebStoreMVC.Domain.Entities;

[Index(nameof(LastName), new [] {nameof(FirstName), nameof(MiddleName), nameof(Age)}, IsUnique = true)]
[Index(nameof(FirstName), new [] {nameof(LastName), nameof(MiddleName), nameof(Age)}, IsUnique = true)]
[Index(nameof(MiddleName), new [] {nameof(LastName), nameof(FirstName), nameof(Age)}, IsUnique = true)]
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
