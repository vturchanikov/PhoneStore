using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PhoneStore.Interfaces;
using PhoneStore.Models;
using PhoneStore.Models.Pages;

namespace PhoneStore.Controllers;

[Authorize(Roles = "Admin")]
[ViewComponent(Name = "Category")]
public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IActionResult Index(QueryOptions options)
    {
        return View(_categoryRepository.GetCategories(options));
    }

    [HttpPost]
    public IActionResult AddCategory(Category category)
    {
        if (ModelState.IsValid)
        {
            _categoryRepository.AddCategory(category);
        }

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

        if (ModelState.IsValid)
        {
            _categoryRepository.UpdateCategory(category);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult DeleteCategory(Category category)
    {
        _categoryRepository.DeleteCategory(category);

        return RedirectToAction(nameof(Index));
    }

    public IViewComponentResult Invoke()
    {
        return new ViewViewComponentResult
        {
            ViewData = new ViewDataDictionary<IEnumerable<Category>>(ViewData, _categoryRepository.Categories)
        };
    }
}
