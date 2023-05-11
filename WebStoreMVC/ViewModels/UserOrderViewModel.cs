namespace WebStoreMVC.ViewModels;

public class UserOrderViewModel
{
    public UserOrderViewModel()
    {
        Address = null!;
        Phone = null!;
    }

    public int Id { get; set; }

    public DateTimeOffset Date { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }

    public string? Description { get; set; }

    public decimal TotalPrice { get; set; }
}