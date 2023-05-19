using Microsoft.AspNetCore.Identity;

using WebStoreMVC.Domain.Entities.Identity;

namespace WebStoreMVC.Interfaces.Services.Identity;

public interface IUsersClient :
	IUserStore<User>,
	IUserRoleStore<User>,
	IUserPasswordStore<User>,
	IUserEmailStore<User>,
	IUserPhoneNumberStore<User>,
	IUserTwoFactorStore<User>,
	IUserLoginStore<User>,
	IUserClaimStore<User>
{
}