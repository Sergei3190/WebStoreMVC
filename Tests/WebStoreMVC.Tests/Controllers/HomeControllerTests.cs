using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using WebStore.Domain;

using WebStoreMVC.Contollers;
using WebStoreMVC.Domain;
using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Tests.Controllers;

public class HomeControllerTests
{
	[Fact]
	public void Index_returns_with_ViewBag_with_products()
	{
		const int total_count = 6;
		var products = new Page<Product>
		(
			Items: Enumerable.Range(1, total_count).Select(id => new Product { Id = id, Name = $"Product-{id}", Section = new() { Name = "Section" } }),
			PageNumber: 1,
			PageSize: total_count,
			TotalCount: total_count
		);

		var productView = new ProductViewModel { };

		var controller = new HomeController();

		var product_data_mock = new Mock<IProductsService>();
		var mapper_mock = new Mock<IMapper>();

		mapper_mock.Setup(m => m.Map<ProductViewModel>(It.IsAny<Product>()))
			.Returns((Product p) => new ProductViewModel()
			{
				Id = p.Id,
				Name = $"Product-{p.Id}",
				Section = new() { Name = "Section" }
			});

		product_data_mock.Setup(s => s.GetProducts(It.IsAny<ProductFilter>()))
		   .Returns(products);

		var result = controller.Index(product_data_mock.Object, mapper_mock.Object);

		var view_result = Assert.IsType<ViewResult>(result);
		var actual_products_result = view_result.ViewData["Products"];

		var actual_products = Assert.IsAssignableFrom<IEnumerable<ProductViewModel>>(actual_products_result);

		Assert.Equal(6, actual_products.Count());
		Assert.Equal(products.Items.Select(p => p.Name).Take(6), actual_products.Select(p => p.Name));

		product_data_mock.Verify(p => p.GetProducts(It.IsAny<ProductFilter>()));
		product_data_mock.VerifyNoOtherCalls();
	}
}
