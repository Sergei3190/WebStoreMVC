using System.Security.Claims;

namespace WebStoreMVC.Dto.Identity;

public class ClaimDto : UserDto
{
	public IEnumerable<Claim> Claims { get; init; } = null!;
}