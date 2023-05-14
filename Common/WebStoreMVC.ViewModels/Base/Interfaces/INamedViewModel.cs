namespace WebStoreMVC.ViewModels.Base.Interfaces;

public interface INamedViewModel : IViewModel
{
    string Name { get; set; }
}