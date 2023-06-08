namespace WebStoreMVC.Dto.Identity;

public class PasswordHashDto : UserDto
{
	public string Hash { get; init; } = null!;
}