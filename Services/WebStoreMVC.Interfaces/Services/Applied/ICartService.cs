using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Interfaces.Services.Applied;
public interface ICartService
{
    void Add(int productId);

    void Decrement(int productId);

    void Remove(int productId);

    public void Clear();

    CartViewModel GetCartViewModel();

    int GetItemsCount();
}