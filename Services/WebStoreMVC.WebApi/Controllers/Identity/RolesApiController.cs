using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebStoreMVC.DAL.Context;
using WebStoreMVC.Domain.Entities.Identity;
using WebStoreMVC.Interfaces;

namespace WebStoreMVC.WebApi.Controllers.Identity;

[ApiController]
[Route(WebApiAddresses.V1.Identity.Roles)]
public class RolesApiController : ControllerBase
{
	private readonly RoleStore<Role> _roleStore;
	private readonly ILogger<RolesApiController> _logger;

	public RolesApiController(WebStoreMVC_DB db, ILogger<RolesApiController> logger)
	{
		_logger = logger;
		_roleStore = new RoleStore<Role>(db);
	}

	[HttpGet("all")]
	public async Task<IEnumerable<Role>> GetAll(CancellationToken cancel = default) => await _roleStore.Roles.ToArrayAsync(cancel);

	[HttpPost]
	public async Task<bool> CreateAsync(Role role)
	{
		var creation_result = await _roleStore.CreateAsync(role);

		if (!creation_result.Succeeded)
			_logger.LogWarning("Ошибка создания роли {0}:{1}",
				role,
				string.Join(", ", creation_result.Errors.Select(e => e.Description)));

		return creation_result.Succeeded;
	}

	[HttpPut]
	public async Task<bool> UpdateAsync(Role role)
	{
		var uprate_result = await _roleStore.UpdateAsync(role);

		if (!uprate_result.Succeeded)
			_logger.LogWarning("Ошибка изменения роли {0}:{1}",
				role,
				string.Join(", ", uprate_result.Errors.Select(e => e.Description)));

		return uprate_result.Succeeded;
	}

	[HttpDelete]
	[HttpPost("delete")]
	public async Task<bool> DeleteAsync(Role role)
	{
		var delete_result = await _roleStore.DeleteAsync(role);

		if (!delete_result.Succeeded)
			_logger.LogWarning("Ошибка удаления роли {0}:{1}",
				role,
				string.Join(", ", delete_result.Errors.Select(e => e.Description)));

		return delete_result.Succeeded;
	}

	[HttpPost("get-role-id")]
	public async Task<string> GetRoleIdAsync([FromBody] Role role) => await _roleStore.GetRoleIdAsync(role);

	[HttpPost("get-role-name")]
	public async Task<string> GetRoleNameAsync([FromBody] Role role) => await _roleStore.GetRoleNameAsync(role);

	[HttpPost("set-role-name/{name}")]
	public async Task<string?> SetRoleNameAsync(Role role, string name)
	{
		await _roleStore.SetRoleNameAsync(role, name);
		await _roleStore.UpdateAsync(role);
		return role.Name;
	}

	[HttpPost("get-normalized-role-name")]
	public async Task<string> GetNormalizedRoleNameAsync(Role role) => await _roleStore.GetNormalizedRoleNameAsync(role);

	[HttpPost("set-normalized-role-name/{name}")]
	public async Task<string?> SetNormalizedRoleNameAsync(Role role, string name)
	{
		await _roleStore.SetNormalizedRoleNameAsync(role, name);
		await _roleStore.UpdateAsync(role);
		return role.NormalizedName;
	}

	[HttpGet("find-by-id/{id}")]
	public async Task<Role> FindByIdAsync(string id) => await _roleStore.FindByIdAsync(id);

	[HttpGet("find-by-name/{name}")]
	public async Task<Role> FindByNameAsync(string name) => await _roleStore.FindByNameAsync(name);
}