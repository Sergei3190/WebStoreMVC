namespace WebStoreMVC.Domain.Entities;

public class CartItem
{
    public CartItem()
    {
        Quantity = 1;
    }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
}