using Microsoft.AspNetCore.Mvc.Rendering;

using WebStore.Domain;

using WebStoreMVC.Domain;
using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.Services.Data;

namespace WebStoreMVC.Services.InMemory
{
	[Obsolete("Используйте InSqlProductsService")]
	public class InMemoryProductsService : IProductsService
	{
		public InMemoryProductsService() { }

		public IEnumerable<Section> GetSections() => TestData.Sections;

		public IEnumerable<Brand> GetBrands() => TestData.Brands;

		public Page<Product> GetProducts(ProductFilter? filter)
		{
			var query = TestData.Products;

			if (filter is { SectionId: { } sectionId })
				query = query.Where(x => x.SectionId == sectionId);

			if (filter is { BrandId: { } brandId })
				query = query.Where(x => x.BrandId == brandId);

			return new(query, 0, 0, 0);
		}

		public Section? GetSectionById(int id)
		{
			throw new NotImplementedException();
		}

		public Brand? GetBrandById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Product?> GetProductById(int id, CancellationToken cancel = default)
		{
			throw new NotImplementedException();
		}

		public Task<bool> EditAsync(Product product, CancellationToken cancel = default)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(int id, CancellationToken cancel = default)
		{
			throw new NotImplementedException();
		}

		public Task<int> AddAsync(Product product, CancellationToken cancel = default)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<SelectListItem>> PopulateSectionDropDownList(CancellationToken cancel = default)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<SelectListItem>> PopulateBrandDropDownList(CancellationToken cancel = default)
		{
			throw new NotImplementedException();
		}
	}
}
