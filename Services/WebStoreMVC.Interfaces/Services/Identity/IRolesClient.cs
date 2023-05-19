using Microsoft.AspNetCore.Identity;

using WebStoreMVC.Domain.Entities.Identity;

namespace WebStoreMVC.Interfaces.Services.Identity;

public interface IRolesClient : IRoleStore<Role>
{

}