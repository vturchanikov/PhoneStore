using Microsoft.AspNetCore.Mvc;
using PhoneShop.Interfaces;
using PhoneShop.Models;
using PhoneShop.Models.Pages;

namespace PhoneShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPhotoService _photoService;

        public ProductController(ICategoryRepository categoryRepository, IProductRepository productRepository, IPhotoService photoService)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _photoService = photoService;
        }

        public ViewResult Index(QueryOptions options, long category = 0) 
        {
            return View(_productRepository.GetProducts(options, category));
        }

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
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            if(product.Image != null)
            {
                var result = await _photoService.AddPhotoAsync(product.Image);
                product.ImageLink = result.Url.ToString();
            }

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
