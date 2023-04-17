using Microsoft.EntityFrameworkCore;

using WebStoreMVC.DAL.Context;

namespace WebStoreMVC.Data;

public class DbInitializer
{
    private readonly WebStoreMVC_DB _db;
    private readonly ILogger<DbInitializer> _logger;

    public DbInitializer(WebStoreMVC_DB db, ILogger<DbInitializer> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task InitializeAsync(bool canRemove, bool canAddTestData, CancellationToken cancel = default)
    {
        _logger.LogInformation("Инициализация БД...");

        if (canRemove)
            await RemoveAsync(cancel).ConfigureAwait(false);

        _logger.LogInformation("Применение миграций БД...");
        await _db.Database.MigrateAsync(cancel).ConfigureAwait(false);
        _logger.LogInformation("Применение миграций БД выполнено");

        if (canAddTestData)
        {
            _logger.LogInformation("Инициализация БД тестовыми данными...");

            await InitializerProductsAsync(cancel).ConfigureAwait(false);
            await InitializerEmployeesAsync(cancel).ConfigureAwait(false);
            await InitializerBlogsAsync(cancel).ConfigureAwait(false);

            _logger.LogInformation("Инициализация БД тестовыми данными выполнена успешно");
        }

        _logger.LogInformation("Инициализация БД выполнена успешно");
    }

    public async Task<bool> RemoveAsync(CancellationToken cancel = default)
    {
        _logger.LogInformation("Удаление БД...");

        var result = await _db.Database.EnsureDeletedAsync(cancel).ConfigureAwait(false);

        _logger.LogInformation("{0}", result ? "Удаление БД выполнено успешно" : "Удаление БД не выполнено");

        return result;
    }

    private async Task InitializerProductsAsync(CancellationToken cancel)
    {
        if (await _db.Products.AnyAsync(cancel).ConfigureAwait(false))
        {
            _logger.LogInformation("Инициализация БД тестовыми данными товаров не требуется");
            return;
        }

        UpdateProductTestData();

        await using var transaction = await _db.Database.BeginTransactionAsync(cancel).ConfigureAwait(false);

        _logger.LogInformation("Добавление в БД секций...");
        await _db.Sections.AddRangeAsync(TestData.Sections, cancel).ConfigureAwait(false);

        _logger.LogInformation("Добавление в БД брендов...");
        await _db.Brands.AddRangeAsync(TestData.Brands, cancel).ConfigureAwait(false);

        _logger.LogInformation("Добавление в БД товаров...");
        await _db.Products.AddRangeAsync(TestData.Products, cancel).ConfigureAwait(false);

        await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        await transaction.CommitAsync(cancel).ConfigureAwait(false);

        _logger.LogInformation("Добавление в БД секций выполнено успешно");
        _logger.LogInformation("Добавление в БД брендов выполнено успешно");
        _logger.LogInformation("Добавление в БД товаров выполнено успешно");
    }

    private async Task InitializerEmployeesAsync(CancellationToken cancel)
    {
        if (await _db.Employees.AnyAsync(cancel).ConfigureAwait(false))
        {
            _logger.LogInformation("Инициализация БД тестовыми данными сотрудников не требуется");
            return;
        }

        UpdateEmployeeTestData();

        _logger.LogInformation("Добавление в БД сотрудников...");
        await _db.Employees.AddRangeAsync(TestData.Employees, cancel).ConfigureAwait(false);
        await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        _logger.LogInformation("Добавление в БД сотрудников выполнено успешно");
    }

    private async Task InitializerBlogsAsync(CancellationToken cancel)
    {
        if (await _db.Blogs.AnyAsync(cancel).ConfigureAwait(false))
        {
            _logger.LogInformation("Инициализация БД тестовыми данными блогов не требуется");
            return;
        }

        UpdateBlogTestData();

        _logger.LogInformation("Добавление в БД блогов...");
        await _db.Blogs.AddRangeAsync(TestData.Blogs, cancel).ConfigureAwait(false);
        await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        _logger.LogInformation("Добавление в БД блогов выполнено успешно");
    }

    private void UpdateProductTestData()
    {
        var sectionsPool = TestData.Sections.ToDictionary(s => s.Id);
        var brandsPool = TestData.Brands.ToDictionary(b => b.Id);

        foreach (var child in TestData.Sections.Where(s => s.ParentId.HasValue))
            child.Parent = sectionsPool[child.ParentId!.Value];

        foreach (var product in TestData.Products)
        {
            product.Section = sectionsPool[product.SectionId];

            if (product.BrandId is { } brandId)
                product.Brand = brandsPool[brandId];

            product.Id = 0;
            product.SectionId = 0;
            product.BrandId = null;
        }

        foreach (var section in TestData.Sections)
            section.Id = 0;

        foreach (var brand in TestData.Brands)
            brand.Id = 0;
    }

    private void UpdateEmployeeTestData()
    {
        foreach (var employee in TestData.Employees)
            employee.Id = 0;
    }

    private void UpdateBlogTestData()
    {
        foreach (var blog in TestData.Blogs)
            blog.Id = 0;
    }
}
