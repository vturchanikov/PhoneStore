using Microsoft.AspNetCore.Mvc;
using PhoneShop.Interfaces;
using PhoneShop.Models;
using PhoneShop.ViewModels;

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
        public async Task<IActionResult> UpdateProduct(UpdateProductViewModel productVM)
        {
            var result = await _photoService.AddPhotoAsync(productVM.Image);
            if (productVM.Id == 0)
            {
                

                var product = new Product
                {
                    Name = productVM.Name,
                    Description = productVM.Description,
                    ShortDescrition = productVM.ShortDescrition,
                    PurchasePrice = productVM.PurchasePrice,
                    RetailPrice = productVM.RetailPrice,
                    Availability = productVM.Availability,
                    ImgaeLink = result.Url.ToString(),
                    CategoryId = productVM.CategoryId
                };

                _productRepository.AddProduct(product);
            }
            else
            {
                var product = new Product
                {
                    Name = productVM.Name,
                    Description = productVM.Description,
                    ShortDescrition = productVM.ShortDescrition,
                    PurchasePrice = productVM.PurchasePrice,
                    RetailPrice = productVM.RetailPrice,
                    Availability = productVM.Availability,
                    ImgaeLink = result.Url.ToString(),
                    CategoryId = productVM.CategoryId
                };

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
