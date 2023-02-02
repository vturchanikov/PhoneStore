namespace PhoneShop.Models;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { set; get; }
    public int Price { get; set; }

    public long CategoryId { get; set; }
    public Category Category { get; set; }

}
