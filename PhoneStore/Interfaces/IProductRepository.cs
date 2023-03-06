using PhoneStore.Models;
using PhoneStore.Models.Pages;

namespace PhoneStore.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> Products { get; }

    PageList<Product> GetProducts(QueryOptions options, long category);

    Product GetProduct(long id);

    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);

    bool Save();
}
