namespace WebStoreMVC.ViewModels;

public class CartViewModel
{
	public CartViewModel()
	{
		Items = new List<(ProductViewModel product, int quantity)>();
		ItemsCount = Items.Sum(i => i.quantity);
		TotalPrice = Items.Sum(i => i.quantity * i.product.Price);
	}

	public IEnumerable<(ProductViewModel product, int quantity)> Items { get; set; }

	public int ItemsCount { get; }

	public decimal TotalPrice { get; }
}