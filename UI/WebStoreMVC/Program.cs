using System.Globalization;

using Microsoft.AspNetCore.Identity;

using WebStoreMVC.Domain.Entities.Identity;
using WebStoreMVC.Infrastructure.Conventions;
using WebStoreMVC.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");

var services = builder.Services;

var config = builder.Configuration;

services.AddIdentity<User, Role>()
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

builder.Services.AddHttpClient("WebStoreMVC.WebApi.Identity", client => 
{
	client.DefaultRequestHeaders.Add("Accept", "application/json");
	client.DefaultRequestHeaders.Add("Accept", "application/xml");

	client.BaseAddress = new Uri(config["WebApi"]);
})
	.AddTypedIdentityClients();

services.AddHttpClient("WebStoreMVC.WebApi", client => 
{
	client.DefaultRequestHeaders.Add("Accept", "application/json");
	client.DefaultRequestHeaders.Add("Accept", "application/xml");

	client.BaseAddress = new Uri(config["WebApi"]);
})
	.AddTypedClients();

services.AddScopedServices();

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

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