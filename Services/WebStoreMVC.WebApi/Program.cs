using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

using WebStore.WebApi.Handlers.Infrastructure;

using WebStoreMVC.DAL.Context;
using WebStoreMVC.Domain.Entities.Identity;
using WebStoreMVC.Services.Data;
using WebStoreMVC.WebApi.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

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

services.AddControllers(opt =>
{
	opt.InputFormatters.Add(new XmlSerializerInputFormatter(opt));
	opt.OutputFormatters.Add(new XmlSerializerOutputFormatter());
});
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScopedServices();

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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandler>();

app.MapControllers();

app.Run();
