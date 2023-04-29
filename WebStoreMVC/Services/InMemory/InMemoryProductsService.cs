﻿using WebStoreMVC.Data;
using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Services.Interfaces;

namespace WebStoreMVC.Services.InMemory
{
    [Obsolete("Используйте InSqlProductsService")]
    public class InMemoryProductsService : IProductsService
    {
        public InMemoryProductsService() { }

        public IEnumerable<Section> GetSections() => TestData.Sections;

        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter? filter)
        {
            var query = TestData.Products;

            if (filter is { SectionId: { } sectionId })
                query = query.Where(x => x.SectionId == sectionId);

            if (filter is { BrandId: { } brandId })
                query = query.Where(x => x.BrandId == brandId);

            return query;
        }

        public Section? GetSectionById(int id)
        {
            throw new NotImplementedException();
        }

        public Brand? GetBrandById(int id)
        {
            throw new NotImplementedException();
        }

        public Product? GetProductById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
