using Microsoft.AspNetCore.Mvc;
using PhoneShop.Interfaces;
using PhoneShop.Models;

namespace PhoneShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public ViewResult Index() =>
            View();

        public ViewResult List(int categoryId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _productRepository.AddProduct(product);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateProduct(long id)
        {
            ViewBag.Categories = _categoryRepository.Categories;

            return View(id == 0 ? new Product() : _productRepository.GetProduct(id));
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (product.Id == 0)
            {
                _productRepository.AddProduct(product);
            }
            else
            {
                _productRepository.UpdateProduct(product);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _productRepository.DeleteProduct(product);

            return RedirectToAction(nameof(Index));
        }
    }
}
