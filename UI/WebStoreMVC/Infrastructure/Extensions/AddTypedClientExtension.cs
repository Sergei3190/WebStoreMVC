using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.Interfaces.TestApi;
using WebStoreMVC.WebApi.Clients.Blogs;
using WebStoreMVC.WebApi.Clients.Employees;
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
				.AddTypedClient<IEmployeesService, EmployeesClient>();
		}
	}
}
