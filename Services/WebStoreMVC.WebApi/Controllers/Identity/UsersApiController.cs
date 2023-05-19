using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebStoreMVC.DAL.Context;

using WebStoreMVC.Domain.Entities.Identity;
using WebStoreMVC.Interfaces;
using WebStoreMVC.Dto.Identity;

namespace WebStore.WebApi.Controllers.Identity;

[ApiController]
[Route(WebApiAddresses.V1.Identity.Users)]
public class UsersApiController : ControllerBase
{
	private readonly UserStore<User, Role, WebStoreMVC_DB> _userStore;
	private readonly ILogger<UsersApiController> _logger;

	public UsersApiController(WebStoreMVC_DB db, ILogger<UsersApiController> logger)
	{
		_logger = logger;
		_userStore = new (db);
	}

	[HttpGet("all")]
	public async Task<IEnumerable<User>> GetAll(CancellationToken cancel = default) => await _userStore.Users.ToArrayAsync(cancel);

	#region Users

	[HttpPost("user-id")]
	public async Task<string> GetUserIdAsync(User user) => await _userStore.GetUserIdAsync(user);

	[HttpPost("user-name")]
	public async Task<string?> GetUserNameAsync(User user) => await _userStore.GetUserNameAsync(user);

	[HttpPost("user-name/{name}")]
	public async Task<string?> SetUserNameAsync([FromBody] User user, string name) 
	{
		await _userStore.SetUserNameAsync(user, name);
		await _userStore.UpdateAsync(user);

		return user.UserName; 
	}

	[HttpPost("normal-user-name")]
	public async Task<string?> GetNormalizedUserNameAsync(User user) => await _userStore.GetNormalizedUserNameAsync(user);

	[HttpPost("normal-user-name/{name}")]
	public async Task<string?> SetNormalizedUserNameAsync([FromBody] User user, string name)
	{
		await _userStore.SetNormalizedUserNameAsync(user, name);
		await _userStore.UpdateAsync(user);

		return user.NormalizedUserName;
	}

	[HttpPost("user")]
	public async Task<bool> CreateAsync([FromBody] User user)
	{
		var result = await _userStore.CreateAsync(user);

		if (!result.Succeeded)
			_logger.LogWarning("Ошибка создания пользователя {0}:{1}", 
				user,
				string.Join(", ", result.Errors.Select(e => e.Description)));

		return result.Succeeded;
	}

	[HttpPut("user")]
	public async Task<bool> UpdateAsync([FromBody] User user)
	{
		var result = await _userStore.UpdateAsync(user);

		if (!result.Succeeded)
			_logger.LogWarning("Ошибка редактирования пользователя {0}:{1}",
				user,
				string.Join(", ", result.Errors.Select(e => e.Description)));

		return result.Succeeded;
	}

	[HttpPost("user/delete")]
	[HttpDelete("user/delete")]
	[HttpDelete]
	public async Task<bool> DeleteAsync([FromBody] User user)
	{
		var result = await _userStore.DeleteAsync(user);

		if (!result.Succeeded)
			_logger.LogWarning("Ошибка удаления пользователя {0}:{1}",
				user,
				string.Join(", ", result.Errors.Select(e => e.Description)));

		return result.Succeeded;
	}

	[HttpGet("user/find/{id}")]
	public async Task<User?> FindByIdAsync(string id) => await _userStore.FindByIdAsync(id);

	[HttpGet("user/normal/{name}")]
	public async Task<User?> FindByNameAsync(string name) => await _userStore.FindByNameAsync(name);

	[HttpPost("role/{role}")]
	public async Task AddToRoleAsync([FromBody] User user, string role)
	{
		await _userStore.AddToRoleAsync(user, role);
		await _userStore.Context.SaveChangesAsync();
	}

	[HttpPost("role/delete/{role}")]
	[HttpDelete("role/{role}")]
	public async Task RemoveToRoleAsync([FromBody] User user, string role)
	{
		await _userStore.RemoveFromRoleAsync(user, role);
		await _userStore.Context.SaveChangesAsync();
	}

	[HttpPost("roles")]
	public async Task<IList<string>> FindByNameAsync([FromBody] User user) => await _userStore.GetRolesAsync(user);

	[HttpPost("in-role/{role}")]
	public async Task<bool> IsInRoleAsync([FromBody] User user, string role) => await _userStore.IsInRoleAsync(user, role);

	[HttpGet("users-in-role/{role}")]
	public async Task<IList<User>> GetUsersInRoleAsync(string role) => await _userStore.GetUsersInRoleAsync(role);

	[HttpPost("get-password-hash")]
	public async Task<string?> GetPasswordHashAsync([FromBody] User user) => await _userStore.GetPasswordHashAsync(user);

	[HttpPost("has-password")]
    public async Task<bool> HasPasswordAsync([FromBody] User user) => await _userStore.HasPasswordAsync(user);

	[HttpPost("set-password-hash")]
	public async Task<string?> RemoveToRoleAsync([FromBody] PasswordHashDto dto)
	{
		await _userStore.SetPasswordHashAsync(dto.User, dto.Hash);
		await _userStore.UpdateAsync(dto.User);
		
		return dto.User.PasswordHash;
	}

	#endregion

	#region Claims

	[HttpPost("get-claims")]
	public async Task<IList<Claim>> GetClaimsAsync([FromBody] User user) => await _userStore.GetClaimsAsync(user);

	[HttpPost("add-claims")]
	public async Task AddClaimsAsync([FromBody] ClaimDto claimInfo)
	{
		await _userStore.AddClaimsAsync(claimInfo.User, claimInfo.Claims);
		await _userStore.Context.SaveChangesAsync();
	}

	[HttpPost("replace-claim")]
	public async Task ReplaceClaimAsync([FromBody] ReplaceClaimDto claimInfo)
	{
		await _userStore.ReplaceClaimAsync(claimInfo.User, claimInfo.OldClaim, claimInfo.NewClaim);
		await _userStore.Context.SaveChangesAsync();
	}

	[HttpPost("remove-claim")]
	public async Task RemoveClaimsAsync([FromBody] ClaimDto claimInfo)
	{
		await _userStore.RemoveClaimsAsync(claimInfo.User, claimInfo.Claims);
		await _userStore.Context.SaveChangesAsync();
	}

	[HttpPost("get-users-for-claim")]
	public async Task<IList<User>> GetUsersForClaimAsync([FromBody] Claim claim) =>
		await _userStore.GetUsersForClaimAsync(claim);

	#endregion

	#region TwoFactor

	[HttpPost("get-two-factor-enabled")]
	public async Task<bool> GetTwoFactorEnabledAsync([FromBody] User user) => await _userStore.GetTwoFactorEnabledAsync(user);

	[HttpPost("set-two-factor/{enable}")]
	public async Task<bool> SetTwoFactorEnabledAsync([FromBody] User user, bool enable)
	{
		await _userStore.SetTwoFactorEnabledAsync(user, enable);
		await _userStore.UpdateAsync(user);
		return user.TwoFactorEnabled;
	}

	#endregion

	#region Email/Phone

	[HttpPost("get-email")]
	public async Task<string?> GetEmailAsync([FromBody] User user) => await _userStore.GetEmailAsync(user);

	[HttpPost("set-email/{email}")]
	public async Task<string> SetEmailAsync([FromBody] User user, string email)
	{
		await _userStore.SetEmailAsync(user, email);
		await _userStore.UpdateAsync(user);
		return user.Email!;
	}

	[HttpPost("get-normalized-email")]
	public async Task<string?> GetNormalizedEmailAsync([FromBody] User user) => await _userStore.GetNormalizedEmailAsync(user);

	[HttpPost("set-normalized-email/{email?}")]
	public async Task<string> SetNormalizedEmailAsync([FromBody] User user, string? email)
	{
		await _userStore.SetNormalizedEmailAsync(user, email);
		await _userStore.UpdateAsync(user);
		return user.NormalizedEmail!;
	}

	[HttpPost("get-email-confirmed")]
	public async Task<bool> GetEmailConfirmedAsync([FromBody] User user) => await _userStore.GetEmailConfirmedAsync(user);

	[HttpPost("set-email-confirmed/{enable}")]
	public async Task<bool> SetEmailConfirmedAsync([FromBody] User user, bool enable)
	{
		await _userStore.SetEmailConfirmedAsync(user, enable);
		await _userStore.UpdateAsync(user);
		return user.EmailConfirmed;
	}

	[HttpGet("user-find-by-email/{email}")]
	public async Task<User> FindByEmailAsync(string email) => await _userStore.FindByEmailAsync(email);

	[HttpPost("get-phone-number")]
	public async Task<string?> GetPhoneNumberAsync([FromBody] User user) => await _userStore.GetPhoneNumberAsync(user);

	[HttpPost("set-phone-number/{phone}")]
	public async Task<string> SetPhoneNumberAsync([FromBody] User user, string phone)
	{
		await _userStore.SetPhoneNumberAsync(user, phone);
		await _userStore.UpdateAsync(user);
		return user.PhoneNumber!;
	}

	[HttpPost("get-phone-number-confirmed")]
	public async Task<bool> GetPhoneNumberConfirmedAsync([FromBody] User user) =>
		await _userStore.GetPhoneNumberConfirmedAsync(user);

	[HttpPost("set-phone-number-confirmed/{confirmed}")]
	public async Task<bool> SetPhoneNumberConfirmedAsync([FromBody] User user, bool confirmed)
	{
		await _userStore.SetPhoneNumberConfirmedAsync(user, confirmed);
		await _userStore.UpdateAsync(user);
		return user.PhoneNumberConfirmed;
	}

	#endregion

	#region Login/Lockout

	[HttpPost("add-login")]
	public async Task AddLoginAsync([FromBody] AddLoginDto login)
	{
		await _userStore.AddLoginAsync(login.User, login.UserLoginInfo);
		await _userStore.Context.SaveChangesAsync();
	}

	[HttpPost("remove-login/{login-provider}/{provider-key}")]
	public async Task RemoveLoginAsync([FromBody] User user, string loginProvider, string providerKey)
	{
		await _userStore.RemoveLoginAsync(user, loginProvider, providerKey);
		await _userStore.Context.SaveChangesAsync();
	}

	[HttpPost("get-logins")]
	public async Task<IList<UserLoginInfo>> GetLoginsAsync([FromBody] User user) => await _userStore.GetLoginsAsync(user);

	[HttpGet("user/find-by-login/{login-provider}/{provider-key}")]
	public async Task<User> FindByLoginAsync(string loginProvider, string providerKey) => await _userStore.FindByLoginAsync(loginProvider, providerKey);

	[HttpPost("get-lockout-end-date")]
	public async Task<DateTimeOffset?> GetLockoutEndDateAsync([FromBody] User user) => await _userStore.GetLockoutEndDateAsync(user);

	[HttpPost("set-lockout-end-date")]
	public async Task<DateTimeOffset?> SetLockoutEndDateAsync([FromBody] SetLockoutDto lockoutInfo)
	{
		await _userStore.SetLockoutEndDateAsync(lockoutInfo.User, lockoutInfo.LockoutEnd);
		await _userStore.UpdateAsync(lockoutInfo.User);
		return lockoutInfo.User.LockoutEnd;
	}

	[HttpPost("increment-access-failed-count")]
	public async Task<int> IncrementAccessFailedCountAsync([FromBody] User user)
	{
		var count = await _userStore.IncrementAccessFailedCountAsync(user);
		await _userStore.UpdateAsync(user);
		return count;
	}

	[HttpPost("reset-access-failed-count")]
	public async Task<int> ResetAccessFailedCountAsync([FromBody] User user)
	{
		await _userStore.ResetAccessFailedCountAsync(user);
		await _userStore.UpdateAsync(user);
		return user.AccessFailedCount;
	}

	[HttpPost("get-access-failed-count")]
	public async Task<int> GetAccessFailedCountAsync([FromBody] User user) => await _userStore.GetAccessFailedCountAsync(user);

	[HttpPost("get-lockout-enabled")]
	public async Task<bool> GetLockoutEnabledAsync([FromBody] User user) => await _userStore.GetLockoutEnabledAsync(user);

	[HttpPost("set-lockout-enabled/{enable}")]
	public async Task<bool> SetLockoutEnabledAsync([FromBody] User user, bool enable)
	{
		await _userStore.SetLockoutEnabledAsync(user, enable);
		await _userStore.UpdateAsync(user);
		return user.LockoutEnabled;
	}

	#endregion
}