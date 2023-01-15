using ProductService.Dtos;

namespace ProductService.AsyncDataService
{
    public interface IMessageBusClient
    {
        void PublishNewProduct(ProductPublishedDto productPublishedDto);
    }
}
