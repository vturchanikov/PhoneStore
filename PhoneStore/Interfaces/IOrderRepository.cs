using PhoneStore.Models;
using PhoneStore.Models.Pages;

namespace PhoneStore.Interfaces;

public interface IOrderRepository
{
    IEnumerable<Order> Orders { get; }

    PageList<Order> GetOrders(QueryOptions options);
    IEnumerable<Order> GetOrdersByUserName(string userName);

    Order GetOrder(long id);

    void AddOrder(Order order);
    void UpdateOrder(Order order);
    void DeleteOrder(Order order);
}
