using PhoneShop.Models;
using PhoneShop.Models.Pages;

namespace PhoneShop.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }

        PageList<Category> GetCategories(QueryOptions options);

        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);

        bool Save();
    }
}
