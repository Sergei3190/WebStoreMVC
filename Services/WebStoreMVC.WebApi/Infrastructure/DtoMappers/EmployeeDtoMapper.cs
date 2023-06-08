using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Dto;

namespace WebStoreMVC.WebApi.Infrastructure.Mappers;

public static class EmployeeDtoMapper
{
	public static EmployeeDto? ToDto(this Employee? employee) => employee is null
		? null
		: new EmployeeDto()
		{
			Id = employee.Id,
			LastName = employee.LastName,
			FirstName = employee.FirstName,
			MiddleName = employee.MiddleName,
			Age = employee.Age,
		};

	public static IEnumerable<EmployeeDto?> ToDto(this IEnumerable<Employee?> employees) => employees is null
		? Enumerable.Empty<EmployeeDto?>()
		: employees.Select(ToDto);
}
