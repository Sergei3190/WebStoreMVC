namespace WebStoreMVC.ViewModels;

public class CartViewModel
{
	public CartViewModel()
	{
		Items = new List<(ProductViewModel product, int quantity)>();
	}

	public IEnumerable<(ProductViewModel product, int quantity)> Items { get; set; }

	public int ItemsCount => Items.Sum(i => i.quantity);

    public decimal TotalPrice => Items.Sum(i => i.quantity * i.product.Price);
}