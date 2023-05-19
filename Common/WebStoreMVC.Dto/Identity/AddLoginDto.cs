using Microsoft.AspNetCore.Identity;

namespace WebStoreMVC.Dto.Identity;

public class AddLoginDto : UserDto
{
	public UserLoginInfo UserLoginInfo { get; init; } = null!;
}