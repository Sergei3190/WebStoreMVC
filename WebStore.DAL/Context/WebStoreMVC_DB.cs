using Microsoft.EntityFrameworkCore;

using WebStoreMVC.Domain.Entities;

namespace WebStoreMVC.DAL.Context;

public class WebStoreMVC_DB : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public WebStoreMVC_DB(DbContextOptions<WebStoreMVC_DB> options)
        : base(options)
    {

    }
}
