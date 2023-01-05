using Microsoft.EntityFrameworkCore;
using StoreService.Models;

namespace StoreService.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions opt):base(opt)
        {

        }

        public DbSet<Store> stores { get; set; }
    }
}
