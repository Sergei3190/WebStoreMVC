using Microsoft.AspNetCore.Identity;

namespace WebStoreMVC.Domain.Identity;

public class User : IdentityUser
{
	public const string Administrator = "Admin";
	public const string AdminPassword = "a_123";
	public override string ToString() => UserName;
}