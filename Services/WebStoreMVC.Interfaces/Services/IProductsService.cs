using Microsoft.AspNetCore.Mvc.Rendering;
using WebStoreMVC.Domain;
using WebStoreMVC.Domain.Entities;

namespace WebStoreMVC.Interfaces.Services;
public interface IProductsService
{
	IEnumerable<Section> GetSections();

	IEnumerable<Brand> GetBrands();

	IEnumerable<Product> GetProducts(ProductFilter? filter = null);

	Section? GetSectionById(int id);

	Brand? GetBrandById(int id);

	Task<Product?> GetProductById(int id, CancellationToken cancel = default);

	Task<int> AddAsync(Product product, CancellationToken cancel = default);

	Task<bool> EditAsync(Product product, CancellationToken cancel = default);

	Task<bool> DeleteAsync(int id, CancellationToken cancel = default);

	Task<IEnumerable<SelectListItem>> PopulateSectionDropDownList(CancellationToken cancel = default);

	Task<IEnumerable<SelectListItem>> PopulateBrandDropDownList(CancellationToken cancel = default);
}