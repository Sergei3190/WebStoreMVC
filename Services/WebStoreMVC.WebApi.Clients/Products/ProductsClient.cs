using System.Net.Http.Json;

using Microsoft.AspNetCore.Mvc.Rendering;

using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Dto;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.WebApi.Clients.Base;
using WebStoreMVC.WebApi.Clients.Infrastrucure.DtoMappers;

namespace WebStoreMVC.WebApi.Clients.Products;

public class ProductsClient : BaseClient, IProductsService
{
	public ProductsClient(HttpClient httpClient)
		: base(httpClient, "api/products")
	{
	}

	public IEnumerable<Section> GetSections()
	{
		var result = Get<IEnumerable<SectionDto>>($"{Address}/sections");
		return result.FromDto();
	}

	public Section? GetSectionById(int id)
	{
		var result = Get<SectionDto>($"{Address}/sections/{id}");
		return result.FromDto();
	}

	public IEnumerable<Brand> GetBrands()
	{
		var result = Get<IEnumerable<BrandDto>>($"{Address}/brands");
		return result.FromDto();
	}

	public Brand? GetBrandById(int id)
	{
		var result = Get<BrandDto>($"{Address}/brands/{id}");
		return result.FromDto();
	}

	public IEnumerable<Product> GetProducts(ProductFilter? filter = null)
	{
		var response = Post($"{Address}/list", filter ?? new());

		if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
			return Enumerable.Empty<Product>();

		var products = response
			.EnsureSuccessStatusCode()
			.Content
			.ReadFromJsonAsync<IEnumerable<ProductDto>>()
			.Result;

		return products.FromDto();
	}

	public async Task<Product?> GetProductById(int id, CancellationToken cancel = default)
	{
		var result = await GetAsync<ProductDto>($"{Address}/{id}", cancel).ConfigureAwait(false);
		return result?.FromDto();
	}

	public async Task<int> AddAsync(Product product, CancellationToken cancel = default)
	{
		ArgumentNullException.ThrowIfNull(nameof(product));

		var dto = product.ToDto();

		var response = await PostAsync(Address, dto, cancel).ConfigureAwait(false);

		var result = await response
			.EnsureSuccessStatusCode()
			.Content
			.ReadFromJsonAsync<ProductDto>(cancellationToken: cancel)
			.ConfigureAwait(false);

		if (result is null)
			throw new InvalidOperationException($"Не удалось добавить товар {product.Name}");

		var id = result.Id;
		product.Id = id;
		return id;
	}

	public async Task<bool> EditAsync(Product product, CancellationToken cancel = default)
	{
		ArgumentNullException.ThrowIfNull(nameof(product));

		var dto = product.ToDto();

		var response = await PutAsync(Address, dto, cancel).ConfigureAwait(false);

		return await response
			.EnsureSuccessStatusCode()
			.Content
			.ReadFromJsonAsync<bool>(cancellationToken: cancel)
			.ConfigureAwait(false);
	}

	public async Task<bool> DeleteAsync(int id, CancellationToken cancel = default)
	{
		var response = await DeleteAsync($"{Address}/{id}", cancel).ConfigureAwait(false);
		var success = response.IsSuccessStatusCode;
		return success;
	}

	public async Task<IEnumerable<SelectListItem>> PopulateSectionDropDownList(CancellationToken cancel = default)
	{
		var result = await GetAsync<IEnumerable<SelectListItem>>($"{Address}/select-sections", cancel).ConfigureAwait(false);
		return result ?? Enumerable.Empty<SelectListItem>();
	}

	public async Task<IEnumerable<SelectListItem>> PopulateBrandDropDownList(CancellationToken cancel = default)
	{
		var result = await GetAsync<IEnumerable<SelectListItem>>($"{Address}/select-brands", cancel).ConfigureAwait(false);
		return result ?? Enumerable.Empty<SelectListItem>();
	}
}