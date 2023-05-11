using WebStoreMVC.Domain.Entities.Orders;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Services.Interfaces;
public interface IOrderService
{
    Task<IEnumerable<Order>> GetUserOrdersAsync(string userName, CancellationToken cancel = default);

    Task<Order?> GetOrderByIdAsync(int id, CancellationToken cancel = default);

    Task<Order> CreateOrderAsync(string userName, CartViewModel cart, OrderViewModel order, CancellationToken cancel = default);
}