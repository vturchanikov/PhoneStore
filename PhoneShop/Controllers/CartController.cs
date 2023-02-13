using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PhoneShop.Infrastructure;
using PhoneShop.Interfaces;
using PhoneShop.Models;

namespace PhoneShop.Controllers;

[ViewComponent(Name = "Cart")]
public class CartController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CartController(IProductRepository productRepository, IOrderRepository orderRepository,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _categoryRepository = categoryRepository;
    }

    public IActionResult Index(string returnUrl)
    {
        ViewBag.returnUrl = returnUrl;

        return View(GetCart());
    }

    [HttpPost]
    public IActionResult AddToCart(Product product, string returnUrl)
    {
        SaveCart(GetCart().AddItem(product, 1));

        return RedirectToAction(nameof(Index), new { returnUrl });
    }

    [HttpPost]
    public IActionResult RemoveFromCart(long productId, string returnUrl)
    {
        SaveCart(GetCart().RemoveItem(productId));

        return RedirectToAction(nameof(Index), new { returnUrl });
    }

    public IActionResult CreateOrder()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateOrder(Order order)
    {
        order.Lines = GetCart().Selections.Select(s => new OrderLine
        {
            ProductId = s.ProductId,
            Quantity = s.Quantity
        }).ToArray();

        _orderRepository.AddOrder(order);
        SaveCart(new Cart());

        return RedirectToAction(nameof(Completed));
    }

    public IActionResult Completed()
    {
        return View();
    }

    private Cart GetCart() =>
        HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();

    private void SaveCart(Cart cart) =>
        HttpContext.Session.SetJson("Cart", cart);

    public IViewComponentResult Invoke(ISession session)
    {
        return new ViewViewComponentResult()
        {
            ViewData = new ViewDataDictionary<Cart>(ViewData, session.GetJson<Cart>("Cart"))
        };
    }
}
