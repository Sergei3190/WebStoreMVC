using System.Security.Claims;

namespace WebStoreMVC.Dto.Identity;

public class ReplaceClaimDto : UserDto
{
	public Claim OldClaim { get; init; } = null!;
	public Claim NewClaim { get; init; } = null!;
}