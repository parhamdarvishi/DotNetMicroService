namespace ProductService.Data
{
    public static class PrepDb
    {

        public static void PrepPopulation(WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }

        }

        public static void SeedData(AppDbContext _context)
        {
            if (!_context.products.Any())
            {
                Console.WriteLine("Seeding data!");
                _context.products.AddRange(
                    new Models.Product() {  Name = "Test",Price = 1000 },
                    new Models.Product() { Name = "Test2", Price = 1500 }
                 );
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Allready have data!");
            }
        }

    }
}
