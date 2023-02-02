using Microsoft.EntityFrameworkCore;
using PhoneShop.Models;

namespace PhoneShop.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
                
        }

        public DbSet<Product> Produts { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

    


}
