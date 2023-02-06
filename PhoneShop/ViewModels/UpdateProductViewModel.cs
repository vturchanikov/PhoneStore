using PhoneShop.Models;

namespace PhoneShop.ViewModels;

public class UpdateProductViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { set; get; }
    public string ShortDescrition { get; set; }
    public int PurchasePrice { get; set; }
    public int RetailPrice { get; set; }
    public bool Availability { get; set; }
    public IFormFile Image { get; set; }

    public long CategoryId { get; set; }
    public Category Category { get; set; }

}
