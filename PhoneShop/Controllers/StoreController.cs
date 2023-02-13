﻿using Microsoft.AspNetCore.Mvc;
using PhoneShop.Interfaces;
using PhoneShop.Models.Pages;

namespace PhoneShop.Controllers;

public class StoreController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public StoreController(ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public IActionResult Index([FromQuery(Name = "options")] QueryOptions productOptions, long category = 0)
    {
        return View(_productRepository.GetProducts(productOptions, category));
    }

    public IActionResult Detail(long id)
    {
        return View(_productRepository.GetProduct(id));
    }
}
