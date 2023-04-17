using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using WebStoreMVC.DAL.Context;
using WebStoreMVC.Data;
using WebStoreMVC.Domain.Identity;
using WebStoreMVC.Services.InSql;
using WebStoreMVC.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<WebStoreMVC_DB>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(opt =>
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

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "GB.WenStore";
    opt.Cookie.HttpOnly = true;

    opt.ExpireTimeSpan = TimeSpan.FromDays(10);

    opt.LoginPath = "/Account/Login";
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";

    opt.SlidingExpiration = true;
});

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IEmployeesService, InSqlEmployeesService>();
builder.Services.AddScoped<IProductsService, InSqlProductsService>();
builder.Services.AddScoped<IBlogsService, InSqlBlogsService>();
builder.Services.AddScoped<DbInitializer>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<WebStoreMVC_DB>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbInitiService = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await dbInitiService.InitializeAsync(
        canRemove: app.Configuration.GetValue("DbRecreate", false),
        canAddTestData: app.Configuration.GetValue("DbAddTestData", false));
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();