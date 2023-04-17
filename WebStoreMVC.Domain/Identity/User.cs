using Microsoft.AspNetCore.Identity;

namespace WebStoreMVC.Domain.Identity;

public class User : IdentityUser
{
    public override string ToString() => UserName;
}