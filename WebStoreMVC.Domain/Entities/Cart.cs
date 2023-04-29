namespace WebStoreMVC.Domain.Entities;

public class Cart
{
	public Cart()
	{
		Items = new HashSet<CartItem>();
		ItemsCount = Items.Sum(i => i.Quantity);
	}

	public ICollection<CartItem> Items { get; set; }

	public int ItemsCount { get; }

	public void Add(int productId)
	{
		var item = Items.FirstOrDefault(i => i.ProductId == productId);
		if (item is null)
		{
			item = new CartItem() { ProductId = productId };
			Items.Add(item);
			return;
		}

		item.Quantity++;
	}

	public void Decrement(int productId)
	{
		var item = Items.FirstOrDefault(i => i.ProductId == productId);
		if (item is null)
			return;

		item.Quantity--;

		if (item.Quantity <= 0)
			Items.Remove(item);
	}

	public void Remove(int productId)
	{
		var item = Items.FirstOrDefault(i => i.ProductId == productId);
		if (item is null)
			return;

		Items.Remove(item);
	}

	public void Clear() => Items.Clear();
}