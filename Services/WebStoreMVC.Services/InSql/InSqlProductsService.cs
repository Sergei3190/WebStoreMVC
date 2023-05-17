using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using WebStoreMVC.DAL.Context;
using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.ViewModels;

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

        public async Task<Product?> GetProductById(int id, CancellationToken cancel = default)
        {
            return await _db.Products
                .Include(p => p.Section)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.Id == id, cancel)
                .ConfigureAwait(false);
        }

        public async Task<int> AddAsync(Product product, CancellationToken cancel = default)
        {
            ArgumentNullException.ThrowIfNull(product);

            var lastItem = await _db.Products
                .OrderBy(p => p.Order)
                .Select(p => new { p.Order })
                .LastOrDefaultAsync(cancel)
                .ConfigureAwait(false);

            if (lastItem is { Order: { } lastOrder })
                product.Order = ++lastOrder;

            await _db.Products.AddAsync(product, cancel).ConfigureAwait(false);

            await _db.SaveChangesAsync(cancel).ConfigureAwait(false);

            _logger.LogInformation("Добавлен товар {0}", product);

            return product.Id;
        }

        public async Task<bool> EditAsync(Product product, CancellationToken cancel = default)
        {
            ArgumentNullException.ThrowIfNull(product);

            var _product = await GetProductById(product.Id, cancel).ConfigureAwait(false);

            if (_product is null)
            {
                _logger.LogWarning("При изменении товара {0} - запись не найдена", product);
                return false;
            }

            _product.Name = product.Name;
            _product.ImageUrl = product.ImageUrl;
            _product.SectionId = product.SectionId;
            _product.BrandId = product.BrandId;
            _product.Price = product.Price;

            await _db.SaveChangesAsync(cancel).ConfigureAwait(false);

            _logger.LogInformation("Изменен товар {0}", product);

            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancel = default)
        {
            var product = await _db.Products
                .Select(e => new Product() { Id = e.Id })
                .FirstOrDefaultAsync(e => e.Id == id, cancel)
                .ConfigureAwait(false);

            if (product is null)
            {
                _logger.LogWarning("При удалении товара с id = {0} - запись не найдена", id);
                return false;
            }

            _db.Products.Remove(product);

            await _db.SaveChangesAsync(cancel).ConfigureAwait(false);

            _logger.LogInformation("Удален товар {0} из корзины", product);

            return true;
        }

        public async Task<IEnumerable<SelectListItem>> PopulateSectionDropDownList(CancellationToken cancel = default)
        {
            var query = from s in _db.Sections
                        orderby s.Name
                        select new SelectListItem()
                        {
                            Text = s.Name,
                            Value = s.Id.ToString()

                        };

            return await query
                .ToArrayAsync(cancel)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<SelectListItem>> PopulateBrandDropDownList(CancellationToken cancel = default)
        {
            var query = from s in _db.Brands
                        orderby s.Name
                        select new SelectListItem()
                        {
                            Text = s.Name,
                            Value = s.Id.ToString()
                        };

            return await query
              .ToArrayAsync(cancel)
              .ConfigureAwait(false);
        }
    }
}
