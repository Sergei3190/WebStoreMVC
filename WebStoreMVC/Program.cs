using Microsoft.EntityFrameworkCore;

using WebStoreMVC.DAL.Context;
using WebStoreMVC.Data;
using WebStoreMVC.Services;
using WebStoreMVC.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IEmployeesService, InMemoryEmployeesService>();
builder.Services.AddScoped<IProductsService, InMemoryProductsService>();
builder.Services.AddScoped<IBlogsService, InMemoryBlogsService>();
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

app.MapGet("/greetings", () => app.Configuration["ServerGreetings"]);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();