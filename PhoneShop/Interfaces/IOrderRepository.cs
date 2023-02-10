using PhoneShop.Models;

namespace PhoneShop.Interfaces;

public interface IOrderRepository
{
    IEnumerable<Order> Orders { get; }

    Order GetOrder(long id);

    void AddOrder(Order order);
    void UpdateOrder(Order order);
    void DeleteOrder(Order order);
}
