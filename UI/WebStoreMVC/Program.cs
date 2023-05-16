using System.Globalization;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using WebStoreMVC.DAL.Context;
using WebStoreMVC.Domain.Entities.Identity;
using WebStoreMVC.Infrastructure.Conventions;
using WebStoreMVC.Infrastructure.Extensions;
using WebStoreMVC.Services.Data;

var builder = WebApplication.CreateBuilder(args);

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");

var services = builder.Services;

var config = builder.Configuration;

var dbType = config["DB:Type"];
var connectionString = config.GetConnectionString(dbType);

switch (dbType)
{
	case "DockerDb":
	case "SqlServer":
		services.AddDbContext<WebStoreMVC_DB>(opt =>
		{
			opt.UseSqlServer(connectionString);
		});
		break;
	case "Sqlite":
		services.AddDbContext<WebStoreMVC_DB>(opt =>
		{
			opt.UseSqlite(connectionString, opt => opt.MigrationsAssembly("WebStoreMVC.DAL.Sqlite"));
		});
		break;
}

services.AddIdentity<User, Role>()
	.AddEntityFrameworkStores<WebStoreMVC_DB>()
	.AddDefaultTokenProviders();

services.Configure<IdentityOptions>(opt =>
{
#if DEBUG
	opt.Password.RequireDigit = false;
	opt.Password.RequireLowercase = false;
	opt.Password.RequireUppercase = false;
	opt.Password.RequireNonAlphanumeric = false;
	opt.Password.RequiredLength = 3;
	opt.Password.RequiredUniqueChars = 3;
#endif

	opt.User.RequireUniqueEmail = false;
	opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

	opt.Lockout.AllowedForNewUsers = false;
	opt.Lockout.MaxFailedAccessAttempts = 10;
	opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
});

services.ConfigureApplicationCookie(opt =>
{
	opt.Cookie.Name = "GB.WebStore_MVC";
	opt.Cookie.HttpOnly = true;

	opt.ExpireTimeSpan = TimeSpan.FromDays(10);

	opt.LoginPath = "/Account/Login";
	opt.LogoutPath = "/Account/Logout";
	opt.AccessDeniedPath = "/Account/AccessDenied";

	opt.SlidingExpiration = true;
});

services.AddControllersWithViews(opt =>
{
	opt.Conventions.Add(new AreasConvension());
});

services.AddScopedServices();

services.AddHttpClient("WebStoreMVC.WebApi", client => client.BaseAddress = new Uri(config["WebApi"]))
	.AddTypedClients();

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dbInitiService = scope.ServiceProvider.GetRequiredService<DbInitializer>();
	await dbInitiService.InitializeAsync(
		canRemove: app.Configuration.GetValue("DB:Recreate", false),
		canAddTestData: app.Configuration.GetValue("DB:AddTestData", false));
}

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/greetings", () => app.Configuration["ServerGreetings"]);

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "areas",
		pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

	endpoints.MapControllerRoute(
		 name: "default",
		 pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();