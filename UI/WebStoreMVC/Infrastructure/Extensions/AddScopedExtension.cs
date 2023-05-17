﻿using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.Interfaces.Services.Applied;
using WebStoreMVC.Services.Data;
using WebStoreMVC.Services.InCookies;
using WebStoreMVC.Services.InSql;
using WebStoreMVC.Services.Shared;

namespace WebStoreMVC.Infrastructure.Extensions
{
    public static class AddScopedExtension
	{
		public static void AddScopedServices(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(nameof(services));

			services
				.AddScoped<ICartService, InCookiesCartService>()
				.AddScoped<IFileService, FileService>();

			services.AddScoped<DbInitializer>();
		}
	}
}
