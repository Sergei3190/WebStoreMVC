namespace WebStoreMVC.ViewModels;

public class BaseViewModel
{
    public BaseViewModel()
    {
        Name = null!;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}