using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Interfaces;
using PhoneStore.Models;
using PhoneStore.Models.Pages;

namespace PhoneStore.Controllers
{
    [Authorize(Roles = "Admin")]
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

        public IActionResult Index(QueryOptions options, long category = 0) 
        {
            return View(_productRepository.GetProducts(options, category));
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
            if (product.CategoryId == null)
                ModelState.AddModelError("", "The category field is required");

            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                {
                    if (product.Image != null)
                    {
                        var result = await _photoService.AddPhotoAsync(product.Image);
                        product.ImageLink = result.Url.ToString();
                    }

                    _productRepository.AddProduct(product);
                }
                else
                {
                    _productRepository.UpdateProduct(product);
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _categoryRepository.Categories;
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _productRepository.DeleteProduct(product);

            return RedirectToAction(nameof(Index));
        }
    }
}
