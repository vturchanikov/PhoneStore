using System.ComponentModel.DataAnnotations;

namespace PhoneStore.Models;

public class Order
{
    public long Id { get; set; }
    
    public string CustomerName { get; set; }
    public string Address { get; set; }
    public string Region { get; set; }
    public string PostalIndex { get; set; }
    public bool Shipped { get; set; }

    public IEnumerable<OrderLine>? Lines { get; set; }
}
