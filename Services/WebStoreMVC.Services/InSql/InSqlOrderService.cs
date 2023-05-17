using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using WebStoreMVC.DAL.Context;
using WebStoreMVC.Domain.Entities.Identity;
using WebStoreMVC.Domain.Entities.Orders;
using WebStoreMVC.Interfaces.Services;
using WebStoreMVC.ViewModels;

namespace WebStoreMVC.Services.InSql
{
    public class InSqlOrderService : IOrderService
    {
        private readonly WebStoreMVC_DB _db;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<InSqlOrderService> _logger;

        public InSqlOrderService(WebStoreMVC_DB db,
            UserManager<User> userManager,
            ILogger<InSqlOrderService> logger)
        {
            _db = db;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userName, CancellationToken cancel = default)
        {
            var orders = await _db.Orders
                .Include(o => o.User)
                .Where(o => o.User.UserName == userName)
                .Include(o => o.Items)
                    .ThenInclude(i =>i.Product)
                .ToArrayAsync(cancel)
                .ConfigureAwait(false);

            return orders;
        }

        public async Task<Order?> GetOrderByIdAsync(int id, CancellationToken cancel = default)
        {
            var order = await _db.Orders
                .Where(o => o.Id == id)
                .Include(o => o.User)
                .Include(o => o.Items)
				   .ThenInclude(i => i.Product)
				.FirstOrDefaultAsync(cancel)
                .ConfigureAwait(false);

            return order;
        }

        public async Task<Order> CreateOrderAsync(string userName, CartViewModel cart, OrderViewModel orderViewModel, CancellationToken cancel = default)
        {
            var user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);

            if (user is null)
                throw new InvalidOperationException($"Пользователь с именем {userName} не найден");

            await using var transaction = await _db.Database.BeginTransactionAsync(cancel).ConfigureAwait(false);

            var order = new Order()
            {
                User = user,
                Address = orderViewModel.Address,
                Phone = orderViewModel.Phone,
                Description = orderViewModel.Description
            };

            var productIds = cart.Items.Select(i => i.product.Id);

            var cartProducts = await _db.Products
                .Where(p => productIds.Contains(p.Id))
                .ToArrayAsync(cancel)
                .ConfigureAwait(false);

            order.Items = cart.Items.Join(cartProducts,
                cartItem => cartItem.product.Id,
                cartProduct => cartProduct.Id,
                (cartItem, cartProduct) => new OrderItem
                {
                    Order = order,
                    Product = cartProduct,
                    Price = cartProduct.Price,
                    Quantity = cartItem.quantity
                })
                .ToArray();

            await _db.Orders.AddAsync(order).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);

            await transaction.CommitAsync(cancel).ConfigureAwait(false);

            return order;
        }
    }
}
