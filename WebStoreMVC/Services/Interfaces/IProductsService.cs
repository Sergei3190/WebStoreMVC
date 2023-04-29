using WebStoreMVC.Domain.Entities;

namespace WebStoreMVC.Services.Interfaces;
public interface IProductsService
{
    IEnumerable<Section> GetSections();

    IEnumerable<Brand> GetBrands();

    IEnumerable<Product> GetProducts(ProductFilter? filter = null);

    Section? GetSectionById(int id);

    Brand? GetBrandById(int id);

    Product? GetProductById(int id);
}