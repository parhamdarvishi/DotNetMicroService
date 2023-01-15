namespace StoreService.Data
{
    public static class PrepData
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
            if (!_context.stores.Any())
            {
                Console.WriteLine("Seeding store service data!");
                _context.stores.AddRange(
                    new Models.Store() { Name = "Store 1", Address = "Tehran,Iran", PostalCode = "1010" , RegisterDate = DateTime.Now.AddDays(-2) },
                    new Models.Store() { Name = "Store 2", Address = "Shiraz,Iran", PostalCode = "2020", RegisterDate = DateTime.Now.AddDays(-1) }
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
