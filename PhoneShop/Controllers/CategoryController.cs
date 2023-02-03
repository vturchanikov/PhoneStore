using Microsoft.AspNetCore.Mvc;
using PhoneShop.Interfaces;
using PhoneShop.Models;

namespace PhoneShop.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IActionResult Index()
    {
        return View(_categoryRepository.Categories);
    }

    [HttpPost]
    public IActionResult AddCategory(Category category)
    {
        _categoryRepository.AddCategory(category);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult EditCategory(long id)
    {
        ViewBag.EditId = id;

        return View("Index", _categoryRepository.Categories);
    }

    [HttpPost]
    public IActionResult UpdateCategory(Category category)
    {
        _categoryRepository.UpdateCategory(category);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult DeleteCategory(Category category)
    {
        _categoryRepository.DeleteCategory(category);

        return RedirectToAction(nameof(Index));
    }
}
