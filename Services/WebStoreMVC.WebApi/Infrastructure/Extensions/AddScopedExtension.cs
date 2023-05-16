using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.Services.Data;
using WebStoreMVC.Services.InSql;

namespace WebStoreMVC.WebApi.Infrastructure.Extensions
{
	public static class AddScopedExtension
	{
		public static void AddScopedServices(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(nameof(services));

			services.AddScoped<IEmployeesService, InSqlEmployeesService>();
			services.AddScoped<IProductsService, InSqlProductsService>();
			services.AddScoped<IOrderService, InSqlOrderService>();
			services.AddScoped<IBlogsService, InSqlBlogsService>();

			services.AddScoped<DbInitializer>();
		}
	}
}
