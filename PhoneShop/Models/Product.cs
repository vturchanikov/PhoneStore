namespace PhoneShop.Models;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { set; get; }
    public int PurchasePrice { get; set; }
    public int RetailPrice { get; set; }
    public bool Availability { get; set; }
    public string ImageLink { get; set; }

    public long CategoryId { get; set; }
    public Category Category { get; set; }  
}
