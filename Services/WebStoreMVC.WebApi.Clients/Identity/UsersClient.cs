using System.Net.Http.Json;
using System.Security.Claims;

using Microsoft.AspNetCore.Identity;

using WebStoreMVC.Domain.Entities.Identity;
using WebStoreMVC.Dto.Identity;
using WebStoreMVC.Interfaces;
using WebStoreMVC.Interfaces.Services.Identity;
using WebStoreMVC.WebApi.Clients.Base;

namespace WebStoreMVC.WebApi.Clients.Identity;

public class UsersClient : BaseClient, IUsersClient
{
	public UsersClient(HttpClient httpClient)
		: base(httpClient, WebApiAddresses.V1.Identity.Users)
	{
	}

	#region Implementation of IUserStore<User>

	public async Task<string> GetUserIdAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/user-id", user, cancel);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task<string?> GetUserNameAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/user-name", user, cancel);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task SetUserNameAsync(User user, string name, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/user-name/{name}", user, cancel);
		user.UserName = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task<string?> GetNormalizedUserNameAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/normal-user-name/", user, cancel);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task SetNormalizedUserNameAsync(User user, string name, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/normal-user-name/{name}", user, cancel);
		user.NormalizedUserName = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/user", user, cancel);
		var creation_success = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel)
		   .ConfigureAwait(false);

		return creation_success
			? IdentityResult.Success
			: IdentityResult.Failed();
	}

	public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancel = default)
	{
		var response = await PutAsync($"{Address}/user", user, cancel);
		var update_result = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel)
		   .ConfigureAwait(false);

		return update_result
			? IdentityResult.Success
			: IdentityResult.Failed();
	}

	public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/user/delete", user, cancel);
		var delete_result = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel)
		   .ConfigureAwait(false);

		return delete_result
			? IdentityResult.Success
			: IdentityResult.Failed();
	}

	public async Task<User?> FindByIdAsync(string id, CancellationToken cancel = default)
	{
		var user = await GetAsync<User>($"{Address}/user/find/{id}", cancel).ConfigureAwait(false);
		return user!;
	}

	public async Task<User?> FindByNameAsync(string name, CancellationToken cancel = default)
	{
		var user = await GetAsync<User>($"{Address}/user/normal/{name}", cancel).ConfigureAwait(false);
		return user!;
	}

	#endregion

	#region Implementation of IUserRoleStore<User>

	public async Task AddToRoleAsync(User user, string role, CancellationToken cancel = default)
	{
		await PostAsync($"{Address}/role/{role}", user, cancel).ConfigureAwait(false);
	}

	public async Task RemoveFromRoleAsync(User user, string role, CancellationToken cancel = default)
	{
		await PostAsync($"{Address}/role/delete/{role}", user, cancel).ConfigureAwait(false);
	}

	public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/roles", user, cancel).ConfigureAwait(false);
		var roles = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<IList<string>>(cancellationToken: cancel)
		   .ConfigureAwait(false);
		return roles!;
	}

	public async Task<bool> IsInRoleAsync(User user, string role, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/in-role/{role}", user, cancel);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel)
		   .ConfigureAwait(false);
	}

	public async Task<IList<User>> GetUsersInRoleAsync(string role, CancellationToken cancel = default)
	{
		var users = await GetAsync<List<User>>($"{Address}/users-in-role/{role}", cancel).ConfigureAwait(false);
		return users!;
	}

	#endregion

	#region Implementation of IUserPasswordStore<User>

	public async Task SetPasswordHashAsync(User user, string hash, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/set-password-hash", new PasswordHashDto { User = user, Hash = hash }, cancel)
		   .ConfigureAwait(false);
		user.PasswordHash = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel);
	}

	public async Task<string?> GetPasswordHashAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/get-password-hash", user, cancel).ConfigureAwait(false);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task<bool> HasPasswordAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/has-password", user, cancel).ConfigureAwait(false);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel)
		   .ConfigureAwait(false);
	}

	#endregion

	#region Implementation of IUserEmailStore<User>

	public async Task SetEmailAsync(User user, string email, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/set-email/{email}", user, cancel).ConfigureAwait(false);
		user.Email = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task<string?> GetEmailAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/get-email", user, cancel).ConfigureAwait(false);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/get-email-confirmed", user, cancel).ConfigureAwait(false);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel)
		   .ConfigureAwait(false);
	}

	public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/set-email-confirmed/{confirmed}", user, cancel).ConfigureAwait(false);
		user.EmailConfirmed = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel)
		   .ConfigureAwait(false);
	}

	public async Task<User?> FindByEmailAsync(string email, CancellationToken cancel = default)
	{
		var user = await GetAsync<User>($"{Address}/user/find-by-email/{email}", cancel).ConfigureAwait(false);
		return user!;
	}

	public async Task<string?> GetNormalizedEmailAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/user/get-normalized-email", user, cancel);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task SetNormalizedEmailAsync(User user, string email, CancellationToken cancel = default)	
	{
		var response = await PostAsync($"{Address}/set-normalized-email/{email}", user, cancel).ConfigureAwait(false);
		user.NormalizedEmail = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	#endregion

	#region Implementation of IUserPhoneNumberStore<User>

	public async Task SetPhoneNumberAsync(User user, string phone, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/set-phone-number/{phone}", user, cancel).ConfigureAwait(false);
		user.PhoneNumber = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task<string?> GetPhoneNumberAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/get-phone-number", user, cancel).ConfigureAwait(false);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadAsStringAsync(cancel)
		   .ConfigureAwait(false);
	}

	public async Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/get-phone-number-confirmed", user, cancel).ConfigureAwait(false);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel)
		   .ConfigureAwait(false);
	}

	public async Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/set-phone-number-confirmed/{confirmed}", user, cancel).ConfigureAwait(false);
		user.PhoneNumberConfirmed = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel)
		   .ConfigureAwait(false);
	}

	#endregion

	#region Implementation of IUserLoginStore<User>

	public async Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancel = default)
	{
		await PostAsync($"{Address}/add-login", new AddLoginDto { User = user, UserLoginInfo = login }, cancel).ConfigureAwait(false);
	}

	public async Task RemoveLoginAsync(User user, string LoginProvider, string ProviderKey, CancellationToken cancel = default)
	{
		await PostAsync($"{Address}/remove-login/{LoginProvider}/{ProviderKey}", user, cancel).ConfigureAwait(false);
	}

	public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/get-logins", user, cancel).ConfigureAwait(false);
		var logins = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<List<UserLoginInfo>>(cancellationToken: cancel)
		   .ConfigureAwait(false);
		return logins!;
	}

	public async Task<User?> FindByLoginAsync(string LoginProvider, string ProviderKey, CancellationToken cancel = default)
	{
		var user = await GetAsync<User>($"{Address}/user/find-by-login/{LoginProvider}/{ProviderKey}", cancel).ConfigureAwait(false);
		return user!;
	}

	#endregion

	#region Implementation of IUserLockoutStore<User>

	public async Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/get-lockout-end-date", user, cancel).ConfigureAwait(false);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<DateTimeOffset?>(cancellationToken: cancel)
		   .ConfigureAwait(false);
	}

	public async Task SetLockoutEndDateAsync(User user, DateTimeOffset? EndDate, CancellationToken cancel = default)
	{
		var response = await PostAsync(
				$"{Address}/set-lockout-end-date",
				new SetLockoutDto { User = user, LockoutEnd = EndDate },
				cancel)
		   .ConfigureAwait(false);
		user.LockoutEnd = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<DateTimeOffset?>(cancellationToken: cancel)
		   .ConfigureAwait(false);
	}

	public async Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/increment-access-failed-count", user, cancel).ConfigureAwait(false);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<int>(cancellationToken: cancel)
		   .ConfigureAwait(false);
	}

	public async Task ResetAccessFailedCountAsync(User user, CancellationToken cancel = default)
	{
		await PostAsync($"{Address}/reset-access-failed-count", user, cancel).ConfigureAwait(false);
	}

	public async Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/get-access-failed-count", user, cancel).ConfigureAwait(false);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<int>(cancellationToken: cancel)
		   .ConfigureAwait(false);
	}

	public async Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/get-lockout-enabled", user, cancel).ConfigureAwait(false);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel)
		   .ConfigureAwait(false);
	}

	public async Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/set-lockout-enabled/{enabled}", user, cancel).ConfigureAwait(false);
		user.LockoutEnabled = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel)
		   .ConfigureAwait(false);
	}

	#endregion

	#region Implementation of IUserTwoFactorStore<User>

	public async Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/set-two-factor/{enabled}", user, cancel).ConfigureAwait(false);
		user.TwoFactorEnabled = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel)
		   .ConfigureAwait(false);
	}

	public async Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/get-two-factor-enabled", user, cancel).ConfigureAwait(false);
		return await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<bool>(cancellationToken: cancel)
		   .ConfigureAwait(false);
	}

	#endregion

	#region Implementation of IUserClaimStore<User>

	public async Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/get-claims", user, cancel).ConfigureAwait(false);
		var claims = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<List<Claim>>(cancellationToken: cancel)
		   .ConfigureAwait(false);
		return claims!;
	}

	public async Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancel = default)
	{
		var response = await PostAsync(
				$"{Address}/add-claims",
				new ClaimDto { User = user, Claims = claims },
				cancel)
		   .ConfigureAwait(false);

		response.EnsureSuccessStatusCode();
	}

	public async Task ReplaceClaimAsync(User user, Claim OldClaim, Claim NewClaim, CancellationToken cancel = default)
	{
		var response = await PostAsync(
				$"{Address}/replace-claim",
				new ReplaceClaimDto { User = user, OldClaim = OldClaim, NewClaim = NewClaim },
				cancel)
		   .ConfigureAwait(false);

		response.EnsureSuccessStatusCode();
	}

	public async Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancel = default)
	{
		var response = await PostAsync(
				$"{Address}/remove-claims",
				new ClaimDto { User = user, Claims = claims },
				cancel)
		   .ConfigureAwait(false);

		response.EnsureSuccessStatusCode();
	}

	public async Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancel = default)
	{
		var response = await PostAsync($"{Address}/get-users-for-claim", claim, cancel).ConfigureAwait(false);
		var users = await response
		   .EnsureSuccessStatusCode()
		   .Content
		   .ReadFromJsonAsync<List<User>>(cancellationToken: cancel)
		   .ConfigureAwait(false);
		return users!;
	}

	#endregion
}