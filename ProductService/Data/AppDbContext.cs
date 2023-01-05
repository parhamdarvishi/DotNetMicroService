using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<Product> products { get; set; }
    }
}
