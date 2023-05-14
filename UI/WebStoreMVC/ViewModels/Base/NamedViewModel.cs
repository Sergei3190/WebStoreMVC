namespace WebStoreMVC.ViewModels.Base;

public class NamedViewModel : ViewModel
{
    public NamedViewModel()
    {
        Name = null!;
    }

    public string Name { get; set; }
}