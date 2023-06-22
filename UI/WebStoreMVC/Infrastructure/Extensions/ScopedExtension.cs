using WebStoreMVC.Interfaces.Services.Applied;
using WebStoreMVC.Services.Applied;

namespace WebStoreMVC.Infrastructure.Extensions
{
	public static class ScopedExtension
	{
		public static void AddScopedServices(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(nameof(services));

			services
				.AddScoped<IFilesService, FilesService>();
		}
	}
}
