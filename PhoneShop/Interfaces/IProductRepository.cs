using PhoneShop.Models;

namespace PhoneShop.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> Products { get; }

    Product GetProduct(long id);

    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);

    bool Save();
}
