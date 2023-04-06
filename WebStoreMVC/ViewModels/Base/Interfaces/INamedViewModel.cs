namespace WebStoreMVC.ViewModels.Base.Interfaces;

public interface INamedViewModel : IEntity
{
    string Name { get; set; }
}