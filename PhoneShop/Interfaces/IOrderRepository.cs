using PhoneShop.Models;
using PhoneShop.Models.Pages;

namespace PhoneShop.Interfaces;

public interface IOrderRepository
{
    IEnumerable<Order> Orders { get; }

    PageList<Order> GetOrders(QueryOptions options);

    Order GetOrder(long id);

    void AddOrder(Order order);
    void UpdateOrder(Order order);
    void DeleteOrder(Order order);
}
