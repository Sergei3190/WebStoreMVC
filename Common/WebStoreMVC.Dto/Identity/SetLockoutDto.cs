namespace WebStoreMVC.Dto.Identity;

public class SetLockoutDto : UserDto
{
	public DateTimeOffset? LockoutEnd { get; init; } = null!;
}