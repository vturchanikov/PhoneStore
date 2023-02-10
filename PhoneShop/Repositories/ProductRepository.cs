﻿using Microsoft.EntityFrameworkCore;
using PhoneShop.Data;
using PhoneShop.Interfaces;
using PhoneShop.Models;
using PhoneShop.Models.Pages;

namespace PhoneShop.Repositories;

public class ProductRepository : Repository, IProductRepository
{
    public ProductRepository(DataContext context) : base(context){}

    public IEnumerable<Product> Products => _context.Products.Include(p => p.Category).ToList();

    public PageList<Product> GetProducts(QueryOptions options)
    {
        return new PageList<Product>(_context.Products.Include(p => p.Category), options);
    }

    public Product GetProduct(long id) =>
        _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);

    public void AddProduct(Product product)
    {
        _context.Products.Add(product);

        Save();
    }

    public void UpdateProduct(Product product)
    {
        _context.Products.Update(product);

        Save();
    }

    public void DeleteProduct(Product product)
    {
        _context.Products.Remove(product);

        Save();
    }
    
}
