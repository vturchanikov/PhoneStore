using Microsoft.EntityFrameworkCore;
using PhoneStore.Data;
using PhoneStore.Interfaces;
using PhoneStore.Models;
using PhoneStore.Models.Pages;

namespace PhoneStore.Repositories
{
    public class OrderRepository : Repository, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context) { }

        public IEnumerable<Order> Orders => _context.Orders
            .Include(o => o.Lines).ThenInclude(l => l.Product);

        public PageList<Order> GetOrders(QueryOptions options)
        {
            return new PageList<Order>(_context.Orders.Include(o => o.Lines).ThenInclude(l => l.Product), options);
        }
        
        public Order GetOrder(long id)
        {
            return _context.Orders
                .Include(o => o.Lines).FirstOrDefault(o => o.Id == id);
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);

            Save();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);

            Save();
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);

            Save();
        }
    }
}
