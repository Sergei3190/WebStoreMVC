namespace WebStoreMVC.ViewModels;

public class CartOrderViewModel
{
	public CartOrderViewModel()
	{
		Cart = null!;
		Order = null!;	
	}

	public CartViewModel Cart { get; set; }
	public OrderViewModel Order { get; set; }
}