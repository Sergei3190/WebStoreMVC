using Microsoft.AspNetCore.Mvc.Rendering;

using WebStoreMVC.Domain.Entities;

namespace WebStoreMVC.Services.Interfaces;
public interface IProductsService
{
	IEnumerable<Section> GetSections();

	IEnumerable<Brand> GetBrands();

	IEnumerable<Product> GetProducts(ProductFilter? filter = null);

	Section? GetSectionById(int id);

	Brand? GetBrandById(int id);

	Task<Product?> GetProductById(int id);

    Task<int> AddAsync(Product product);

    Task<bool> EditAsync(Product product);

	Task<bool> DeleteAsync(int id);

	SelectList PopulateSectionDropDownList(object? selectedSection = null);

	SelectList PopulateBrandDropDownList(object? selectedBrand = null);
}