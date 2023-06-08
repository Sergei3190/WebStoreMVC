using System.Net.Http.Json;

using Microsoft.AspNetCore.Identity;

using WebStoreMVC.Domain.Entities.Identity;
using WebStoreMVC.Interfaces;
using WebStoreMVC.Interfaces.Services.Identity;
using WebStoreMVC.WebApi.Clients.Base;

namespace WebStoreMVC.WebApi.Clients.Identity;

public class RolesClient : BaseClient, IRolesClient
{
	public RolesClient(HttpClient httpClient)
		: base(httpClient, WebApiAddresses.V1.Identity.Roles)
	{
	}

	#region IRoleStore<Role>

	public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancel = default)
	{
		var response = await PostAsync(Address, role, cancel).ConfigureAwait(false);
		var result = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel).ConfigureAwait(false);

		return result
			? IdentityResult.Success
			: IdentityResult.Failed();
	}

	public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancel = default)
	{
		var response = await PutAsync(Address, role, cancel).ConfigureAwait(false);
		var result = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel).ConfigureAwait(false);

		return result
			? IdentityResult.Success
			: IdentityResult.Failed();
	}

	public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/delete", role, cancel).ConfigureAwait(false);
		var result = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel).ConfigureAwait(false);

		return result
			? IdentityResult.Success
			: IdentityResult.Failed();
	}

	public async Task<string> GetRoleIdAsync(Role role, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/get-role-id", role, cancel).ConfigureAwait(false);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task<string?> GetRoleNameAsync(Role role, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/get-role-name", role, cancel).ConfigureAwait(false);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task SetRoleNameAsync(Role role, string? name, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/set-role-name/{name}", role, cancel).ConfigureAwait(false);
		role.Name = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task<string?> GetNormalizedRoleNameAsync(Role role, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/get-normalized-role-name", role, cancel).ConfigureAwait(false);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task SetNormalizedRoleNameAsync(Role role, string? name, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/set-normalized-role-name/{name}", role, cancel).ConfigureAwait(false);
		role.NormalizedName = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task<Role?> FindByIdAsync(string id, CancellationToken cancel = default)
	{
		var role = await GetAsync<Role>($"{Address}/find-by-id/{id}", cancel).ConfigureAwait(false);
		return role!;
	}

	public async Task<Role?> FindByNameAsync(string name, CancellationToken cancel = default)
	{
		var role = await GetAsync<Role>($"{Address}/find-by-name/{name}", cancel).ConfigureAwait(false);
		return role!;
	}

	#endregion
}