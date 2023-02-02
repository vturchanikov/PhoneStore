using Microsoft.AspNetCore.Mvc;
using PhoneShop.Interfaces;

namespace PhoneShop.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public ViewResult Index()
    {
        return View(_categoryRepository.Categories);
    }
}
