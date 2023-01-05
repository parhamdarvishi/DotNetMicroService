using StoreService.Models;

namespace StoreService.Data
{
    public interface IStoreRepo
    {
        bool SaveChange();

        IEnumerable<Store> GetAllStores();
        Store GetStoreById(int id);
        void CreateStore(Store store);
    }
}
