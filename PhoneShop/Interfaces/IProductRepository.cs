using PhoneShop.Models;
using PhoneShop.Models.Pages;

namespace PhoneShop.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> Products { get; }

    PageList<Product> GetProducts(QueryOptions options);

    Product GetProduct(long id);

    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);

    bool Save();
}
