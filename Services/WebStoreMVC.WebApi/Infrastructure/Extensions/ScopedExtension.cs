using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.Interfaces.Services.Applied;
using WebStoreMVC.Services.Data;
using WebStoreMVC.Services.InCookies;
using WebStoreMVC.Services.InSql;

namespace WebStoreMVC.WebApi.Infrastructure.Extensions
{
	public static class ScopedExtension
	{
		public static void AddScopedServices(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(nameof(services));

			services
				.AddScoped<IEmployeesService, InSqlEmployeesService>()
				.AddScoped<IProductsService, InSqlProductsService>()
				.AddScoped<IOrderService, InSqlOrderService>()
				.AddScoped<IBlogsService, InSqlBlogsService>()
				.AddScoped<ICartStore, InCookiesCartStore>()
				.AddScoped<ICartService, InCookiesCartService>();

			services.AddScoped<DbInitializer>();
		}
	}
}
