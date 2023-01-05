using StoreService.Models;

namespace StoreService.Data
{
    public class StoreRepo : IStoreRepo
    {
        private readonly AppDbContext _context;
        public StoreRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateStore(Store store)
        {
            if (store == null)
            {
                throw new ArgumentNullException();
            }

            _context.stores.Add(store);
        }

        public IEnumerable<Store> GetAllStores()
        {
            return _context.stores.ToList();
        }

        public Store GetStoreById(int id)
        {
            return _context.stores.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChange()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}
