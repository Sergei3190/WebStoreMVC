using WebStoreMVC.DAL.Context;
using WebStoreMVC.Data;
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
        public IEnumerable<Brand> GetBrands() => _db.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter? filter)
        {
            var query = _db.Products.AsQueryable();

            if (filter is { SectionId: { } sectionId })
                query = query.Where(x => x.SectionId == sectionId);

            if (filter is { BrandId: { } brandId })
                query = query.Where(x => x.BrandId == brandId);

            return query;
        }
    }
}
