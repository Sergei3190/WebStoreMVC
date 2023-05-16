using WebStoreMVC.Domain.Entities;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Infrastructure.Mappings;

public static class EmployeesMapper
{
	public static EmployeeViewModel? ToView(this Employee? employee) => employee is null
		? null
		: new EmployeeViewModel()
		{
			Id = employee.Id,
			LastName = employee.LastName,
			FirstName = employee.FirstName,
			MiddleName = employee.MiddleName,
			Age = employee.Age,
		};

	public static Employee? FromView(this EmployeeViewModel? viewModel) => viewModel is null
		? null
		: new Employee()
		{
			Id = viewModel.Id,
			LastName = viewModel.LastName,
			FirstName = viewModel.FirstName,
			MiddleName = viewModel.MiddleName,
			Age = viewModel.Age,
		};

	public static IEnumerable<EmployeeViewModel?> ToView(this IEnumerable<Employee?> employees) => employees is null
		? Enumerable.Empty<EmployeeViewModel?>()
		: employees.Select(ToView);

	public static IEnumerable<Employee?> FromView(this IEnumerable<EmployeeViewModel?> viewModels) => viewModels is null
		? Enumerable.Empty<Employee?>()
		: viewModels.Select(FromView);
}
