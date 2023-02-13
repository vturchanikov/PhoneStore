using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneShop.Models;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { set; get; }
    public string ShortDescrition { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Purchase price must be higher than zero")]
    public int PurchasePrice { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Retail price must be higher than zero")]
    public int RetailPrice { get; set; }

    public bool Availability { get; set; }
    public string? ImageLink { get; set; }

    [NotMapped]
    public IFormFile Image { get; set; }

    [Display(Name = "category")]
    public long? CategoryId { get; set; }
    public Category? Category { get; set; }  
}
