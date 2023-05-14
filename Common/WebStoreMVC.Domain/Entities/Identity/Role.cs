using Microsoft.AspNetCore.Identity;

namespace WebStoreMVC.Domain.Entities.Identity;

public class Role : IdentityRole
{
    public const string Administrations = "Administrations";
    public const string Users = "Users";
    public override string ToString() => Name;
}