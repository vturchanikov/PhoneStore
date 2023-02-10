using Microsoft.AspNetCore.Mvc;
using PhoneShop.Interfaces;
using PhoneShop.Models;
using PhoneShop.Models.Pages;

namespace PhoneShop.Controllers;

public class OrderController : Controller
{
    private IProductRepository _productRepository;
    private IOrderRepository _orderRepository;

    public OrderController(IProductRepository productRepository, IOrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public IActionResult Index(QueryOptions options) 
        => View(_orderRepository.GetOrders(options));

    public IActionResult EditOrder(long id)
    {
        var products = _productRepository.Products;

        Order order = id == 0 ? new Order() : _orderRepository.GetOrder(id);

        IDictionary<long, OrderLine> linesMap =
            order.Lines?.ToDictionary(l => l.ProductId) ?? new Dictionary<long, OrderLine>();

        ViewBag.Lines = products
            .Select(p => linesMap.ContainsKey(p.Id)
            ? linesMap[p.Id] : new OrderLine { Product = p, ProductId = p.Id, Quantity = 0 });

        return View(order);
    }

    [HttpPost]
    public IActionResult AddOrUpdateOrder(Order order)
    {
        order.Lines = order.Lines
            .Where(l => l.Id > 0 || (l.Id == 0 && l.Quantity > 0)).ToArray();

        if (order.Id == 0)
        {
            _orderRepository.AddOrder(order);
        }
        else
        {
            _orderRepository.UpdateOrder(order);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult DeleteOrder(Order order)
    {
        _orderRepository.DeleteOrder(order);

        return RedirectToAction(nameof(Index));
    }
}