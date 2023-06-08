using System.Net.Http.Json;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Dto;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.WebApi.Clients.Base;
using WebStoreMVC.WebApi.Clients.Infrastructure.Mappers;

namespace WebStoreMVC.WebApi.Clients.Employees;

public class EmployeesClient : BaseClient, IEmployeesService
{
	public EmployeesClient(HttpClient httpClient)
		: base(httpClient, "api/employees")
	{
	}

	public int GetCount()
	{
		var result = Get<int>($"{Address}/count");
		return result;
	}

	public IEnumerable<Employee> GetAll()
	{
		var result = Get<IEnumerable<EmployeeDto>>(Address);
		return (result?.FromDto() ?? Enumerable.Empty<Employee>())!;
	}

	public IEnumerable<Employee> Get(int skip, int take)
	{
		var result = Get<IEnumerable<EmployeeDto>>($"{Address}({skip}{take})");
		return (result?.FromDto() ?? Enumerable.Empty<Employee>())!;
	}

	public async Task<Employee?> GetByIdAsync(int id)
	{
		var result = await GetAsync<EmployeeDto>($"{Address}/{id}").ConfigureAwait(false);
		return result.FromDto();
	}

	public async Task<int> AddAsync(Employee employee)
	{
		ArgumentNullException.ThrowIfNull(nameof(employee));

		var response = await PostAsync(Address, employee).ConfigureAwait(false);

		var addedEmployee = await response
			.EnsureSuccessStatusCode()
			.Content
			.ReadFromJsonAsync<Employee>()
			.ConfigureAwait(false);

		if (addedEmployee is null)
			throw new InvalidOperationException("Не удалось добавить сотрудника");

		var id = addedEmployee.Id;
		employee.Id = id;
		return id;
	}

	public async Task<bool> EditAsync(Employee employee)
	{
		ArgumentNullException.ThrowIfNull(nameof(employee));

		var response = await PutAsync(Address, employee).ConfigureAwait(false);

		return await response
			.EnsureSuccessStatusCode()
			.Content
			.ReadFromJsonAsync<bool>()
			.ConfigureAwait(false);
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var response = await DeleteAsync($"{Address}/{id}").ConfigureAwait(false);
		var success = response.IsSuccessStatusCode;
		return success;
	}
}