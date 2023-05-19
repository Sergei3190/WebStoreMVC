using Microsoft.AspNetCore.Identity;

using WebStoreMVC.Domain.Entities.Identity;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.Interfaces.Services.Identity;
using WebStoreMVC.Interfaces.TestApi;
using WebStoreMVC.WebApi.Clients.Blogs;
using WebStoreMVC.WebApi.Clients.Employees;
using WebStoreMVC.WebApi.Clients.Identity;
using WebStoreMVC.WebApi.Clients.Orders;
using WebStoreMVC.WebApi.Clients.Products;
using WebStoreMVC.WebApi.Clients.Values;

namespace WebStoreMVC.Infrastructure.Extensions
{
	public static class AddTypedClientExtension
	{
		public static void AddTypedClients(this IHttpClientBuilder clientBuilder)
		{
			ArgumentNullException.ThrowIfNull(nameof(clientBuilder));

			clientBuilder
				.AddTypedClient<IValueService, ValuesClient>()
				.AddTypedClient<IProductsService, ProductsClient>()
				.AddTypedClient<IOrderService, OrdersClient>()
				.AddTypedClient<IBlogsService, BlogsClient>()
				.AddTypedClient<IEmployeesService, EmployeesClient>()
				.AddPolicyHandlers();
		}

		public static void AddTypedIdentityClients(this IHttpClientBuilder clientBuilder)
		{
			ArgumentNullException.ThrowIfNull(nameof(clientBuilder));

			clientBuilder
				.AddTypedClient<IUsersClient, UsersClient>()
				.AddTypedClient<IUserStore<User>, UsersClient>()
				.AddTypedClient<IUserRoleStore<User>, UsersClient>()
				.AddTypedClient<IUserPasswordStore<User>, UsersClient>()
				.AddTypedClient<IUserEmailStore<User>, UsersClient>()
				.AddTypedClient<IUserPhoneNumberStore<User>, UsersClient>()
				.AddTypedClient<IUserTwoFactorStore<User>, UsersClient>()
				.AddTypedClient<IUserLoginStore<User>, UsersClient>()
				.AddTypedClient<IUserClaimStore<User>, UsersClient>()
				.AddTypedClient<IRolesClient, RolesClient>()
				.AddTypedClient<IRoleStore<Role>, RolesClient>()
				.AddPolicyHandlers();
		}
	}
}
