using Microsoft.EntityFrameworkCore;

using WebStoreMVC.DAL.Context;
using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Services.Interfaces;

namespace WebStoreMVC.Services.InSql
{
    public class InSqlProductsService : IProductsService
    {
        private readonly WebStoreMVC_DB _db;
        private readonly ILogger<InSqlProductsService> _logger;

        public InSqlProductsService(WebStoreMVC_DB db, ILogger<InSqlProductsService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IEnumerable<Section> GetSections() => _db.Sections;

        public Section? GetSectionById(int id)
        {
            return _db.Sections
                .Include(p => p.Products)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Brand> GetBrands() => _db.Brands;

        public Brand? GetBrandById(int id)
        {
            return _db.Brands
                .Include(p => p.Products)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetProducts(ProductFilter? filter)
        {
            var query = _db.Products
               .Include(p => p.Section)
               .Include(p => p.Brand)
               .AsQueryable();

            if (filter is { Ids: { Length: > 0 } ids })
                query = query.Where(x => ids.Contains(x.Id));
            else
            {
                if (filter is { SectionId: { } sectionId })
                    query = query.Where(x => x.SectionId == sectionId);

                if (filter is { BrandId: { } brandId })
                    query = query.Where(x => x.BrandId == brandId);
            }

            return query;
        }

        public Product? GetProductById(int id)
        {
            return _db.Products
                .Include(p => p.Section)
                .Include(p => p.Brand)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
