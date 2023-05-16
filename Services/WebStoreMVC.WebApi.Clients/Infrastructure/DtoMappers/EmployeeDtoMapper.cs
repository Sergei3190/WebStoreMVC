using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Dto;

namespace WebStoreMVC.WebApi.Infrastructure.Mappers;

public static class EmployeeDtoMapper
{
	public static Employee? FromDto(this EmployeeDto? viewModel) => viewModel is null
		? null
		: new Employee()
		{
			Id = viewModel.Id,
			LastName = viewModel.LastName,
			FirstName = viewModel.FirstName,
			MiddleName = viewModel.MiddleName,
			Age = viewModel.Age,
		};

	public static IEnumerable<Employee?> FromDto(this IEnumerable<EmployeeDto?> viewModels) => viewModels is null
		? Enumerable.Empty<Employee?>()
		: viewModels.Select(FromDto);
}
