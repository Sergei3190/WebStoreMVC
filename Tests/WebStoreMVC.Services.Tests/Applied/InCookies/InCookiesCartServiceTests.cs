using Microsoft.Extensions.Logging;

using WebStore.Domain;

using WebStoreMVC.Domain;
using WebStoreMVC.Domain.Entities;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.Interfaces.Services.Applied;
using WebStoreMVC.Services.InCookies;
using WebStoreMVC.ViewModels;

using Assert = Xunit.Assert;

namespace WebStoreMVC.Services.Tests.InCookies;

[TestClass]
public class InCookiesCartServiceTests
{
	private Cart _cart = null!;
	private CartViewModel _cart_view_model = null!;

	private Mock<IProductsService> _product_data_mock = null!;
	private Mock<ICartStore> _cart_store_mock = null!;
	private Mock<ILogger<InCookiesCartService>> _logger = null!;

	private ICartService _cart_service = null!;

	[TestInitialize]
	public void Initialize()
	{
		_cart = new Cart
		{
			Items = new List<CartItem>
			{
				new() { ProductId = 1, Quantity = 1 },
				new() { ProductId = 2, Quantity = 3 },
			}
		};

		_cart_view_model = new CartViewModel
		{
			Items = new[]
			{
				( new ProductViewModel { Id = 1, Name = "Product 1", Price = 0.5m }, 1 ),
				( new ProductViewModel { Id = 2, Name = "Product 2", Price = 1.5m }, 3 ),
			}
		};

		_logger = new Mock<ILogger<InCookiesCartService>>();

		_product_data_mock = new Mock<IProductsService>();
		_product_data_mock
		   .Setup(c => c.GetProducts(It.IsAny<ProductFilter>()))
		   .Returns(new Page<Product>(new[]
			{
				new Product
				{
					Id = 1,
					Name = "Product 1",
					Price = 1.1m,
					Order = 1,
					ImageUrl = "img_1.png",
					Brand = new Brand { Id = 1, Name = "Brand 1", Order = 1},
					SectionId = 1,
					Section = new Section{ Id = 1, Name = "Section 1", Order = 1 },
				},
				new Product
				{
					Id = 2,
					Name = "Product 2",
					Price = 2.2m,
					Order = 2,
					ImageUrl = "img_2.png",
					Brand = new Brand { Id = 2, Name = "Brand 2", Order = 2},
					SectionId = 2,
					Section = new Section{ Id = 2, Name = "Section 2", Order = 2 },
				},
				new Product
				{
					Id = 3,
					Name = "Product 3",
					Price = 3.3m,
					Order = 3,
					ImageUrl = "img_3.png",
					Brand = new Brand { Id = 3, Name = "Brand 3", Order = 3},
					SectionId = 3,
					Section = new Section{ Id = 3, Name = "Section 3", Order = 3 },
				},
			}, 1, 3, 3));

		_cart_store_mock = new Mock<ICartStore>();
		_cart_store_mock.Setup(c => c.Cart).Returns(_cart);

		_cart_service = new InCookiesCartService(_cart_store_mock.Object, _product_data_mock.Object, _logger.Object);
	}


	[TestMethod, Description("Тест модели корзины")]
	public void Cart_Class_ItemsCount_returns_Correct_Quantity()
	{
		var cart = _cart;
		var expected_items_count = cart.Items.Sum(i => i.Quantity);

		var actual_items_count = cart.ItemsCount;

		Assert.Equal(expected_items_count, actual_items_count);
	}

	[TestMethod]
	public void CartViewModel_Returns_Correct_ItemsCount()
	{
		var cart_view_model = _cart_view_model;

		var expected_items_count = cart_view_model.Items.Sum(i => i.quantity);

		var actual_items_count = cart_view_model.ItemsCount;

		Assert.Equal(expected_items_count, actual_items_count);
	}

	[TestMethod]
	public void CartViewModel_Returns_Correct_TotalPrice()
	{
		var cart_view_model = _cart_view_model;

		var expected_total_price = cart_view_model.Items.Sum(i => i.quantity * i.product.Price);

		var actual_total_price = cart_view_model.TotalPrice;

		Assert.Equal(expected_total_price, actual_total_price);
	}

	[TestMethod]
	public void CartService_Add_WorkCorrect()
	{
		_cart.Items.Clear();

		const int expected_id = 5;
		const int expected_items_count = 1;

		_cart_service.Add(expected_id);

		var actual_items_count = _cart.ItemsCount;

		Assert.Equal(expected_items_count, actual_items_count);

		Assert.Single(_cart.Items);

		Assert.Equal(expected_id, _cart.Items.Single().ProductId);
	}

	[TestMethod]
	public void CartService_Remove_Correct_Item()
	{
		const int item_id = 1;
		const int expected_product_id = 2;

		_cart_service.Remove(item_id);

		Assert.Single(_cart.Items);

		Assert.Equal(expected_product_id, _cart.Items.Single().ProductId);
	}

	[TestMethod]
	public void CartService_Clear_ClearCart()
	{
		_cart_service.Clear();

		Assert.Empty(_cart.Items);
	}

	[TestMethod]
	public void CartService_Decrement_Correct()
	{
		const int item_id = 2;

		const int expected_quantity = 2;
		const int expected_items_count = 3;
		const int expected_products_count = 2;

		_cart_service.Decrement(item_id);

		Assert.Equal(expected_items_count, _cart.ItemsCount);
		Assert.Equal(expected_products_count, _cart.Items.Count);

		var items = _cart.Items.ToArray();
		Assert.Equal(item_id, items[1].ProductId);
		Assert.Equal(expected_quantity, items[1].Quantity);
	}

	[TestMethod]
	public void CartService_Remove_Item_When_Decrement_to_0()
	{
		const int item_id = 1;
		const int expected_items_count = 3;

		_cart_service.Decrement(item_id);

		Assert.Equal(expected_items_count, _cart.ItemsCount);
		Assert.Single(_cart.Items);
	}

	[TestMethod]
	public void CartService_GetViewModel_WorkCorrect()
	{
		const int expected_items_count = 4;
		const decimal expected_first_product_price = 1.1m;

		var result = _cart_service.GetCartViewModel();

		Assert.Equal(expected_items_count, result.ItemsCount);

		Assert.Equal(expected_first_product_price, result.Items.First().product.Price);

		_product_data_mock.Verify(s => s.GetProducts(It.IsAny<ProductFilter>()));
		_product_data_mock.VerifyNoOtherCalls();
	}
}
