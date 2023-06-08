using WebStoreMVC.Domain.Entities.Identity;

namespace WebStoreMVC.Dto.Identity;
public abstract class UserDto
{
    public User User { get; init; } = null!;
}