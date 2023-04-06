using WebStoreMVC.Domain.Entities;

namespace WebStoreMVC.Services.Interfaces;
public interface IProductsService
{
    IEnumerable<Section> GetSections();

    IEnumerable<Brand> GetBrands();

    IEnumerable<Product> GetProducts(ProductFilter? filter = null); 
}