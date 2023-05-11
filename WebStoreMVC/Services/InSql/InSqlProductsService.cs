using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<Product?> GetProductById(int id)
        {
            return await _db.Products
                .Include(p => p.Section)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<int> AddAsync(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            await _db.Products.AddAsync(product).ConfigureAwait(false);

            await _db.SaveChangesAsync().ConfigureAwait(false);

            _logger.LogInformation("Добавлен товар {0}", product);

            return product.Id;
        }

        public async Task<bool> EditAsync(Product product)
		{
			ArgumentNullException.ThrowIfNull(product);

			//var _product = await GetByIdAsync(product.Id).ConfigureAwait(false);
			//if (_product is null)
			//{
			//	_logger.LogWarning("При изменении товара {0} - запись не найдена", product);
			//	return false;
			//}

			//_product.Age = product.Age;
			//_product.LastName = product.LastName;
			//_product.FirstName = product.FirstName;
			//_product.MiddleName = product.MiddleName;

			//await _db.SaveChangesAsync().ConfigureAwait(false);

			//_logger.LogInformation("Изменен товар {0}", product);

			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{            
            var product = await _db.Products
			    .Select(e => new Product() { Id = e.Id })
			    .FirstOrDefaultAsync(e => e.Id == id)
			    .ConfigureAwait(false);

			if (product is null)
			{
				_logger.LogWarning("При удалении товара с id = {0} - запись не найдена", id);
				return false;
			}

			_db.Products.Remove(product);

			await _db.SaveChangesAsync().ConfigureAwait(false);

			_logger.LogInformation("Удален товар {0} из корзины", product);

			return true;
		}

        public SelectList PopulateSectionDropDownList(object? selectedSection = null)
        {
            var query = from s in _db.Sections
                        orderby s.Name
                        select s;

            return new SelectList(query.AsNoTracking(), "SectionId", "Name", selectedSection);
        }

        public SelectList PopulateBrandDropDownList(object? selectedBrand = null)
        {
            var query = from s in _db.Brands
                        orderby s.Name
                        select s;

            return new SelectList(query.AsNoTracking(), "BrandId", "Name", selectedBrand);
        }
    }
}
