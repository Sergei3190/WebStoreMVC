using WebStoreMVC.Interfaces.Services.Applied;
using WebStoreMVC.Services.Applied;
using WebStoreMVC.Services.InCookies;

namespace WebStoreMVC.Infrastructure.Extensions
{
	public static class AddScopedExtension
	{
		public static void AddScopedServices(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(nameof(services));

			services
				.AddScoped<ICartService, InCookiesCartService>()
				.AddScoped<IFilesService, FilesService>();
		}
	}
}
