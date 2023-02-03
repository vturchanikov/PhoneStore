using PhoneShop.Models;

namespace PhoneShop.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }

        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);

        bool Save();
    }
}
