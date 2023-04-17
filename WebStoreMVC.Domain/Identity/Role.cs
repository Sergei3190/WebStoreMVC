using Microsoft.AspNetCore.Identity;

namespace WebStoreMVC.Domain.Identity;

public class Role: IdentityRole
{
    public override string ToString() => Name;
}