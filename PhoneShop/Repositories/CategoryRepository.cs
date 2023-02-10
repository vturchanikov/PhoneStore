﻿using PhoneShop.Data;
using PhoneShop.Interfaces;
using PhoneShop.Models;
using PhoneShop.Models.Pages;

namespace PhoneShop.Repositories
{
    public class CategoryRepository : Repository, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context) { }

        public IEnumerable<Category> Categories => _context.Categories.ToList();

        public PageList<Category> GetCategories(QueryOptions options)
        {
            return new PageList<Category>(_context.Categories, options);
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);

            Save();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);

            Save();
        }

        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);

            Save();
        }
    }
}
