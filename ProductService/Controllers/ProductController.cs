using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductService.AsyncDataService;
using ProductService.Data;
using ProductService.Dtos;
using ProductService.Models;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepo _repo;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;

        public ProductController(IProductRepo repo, IMapper mapper, IMessageBusClient messageBusClient)
        {
            _mapper = mapper;
            _repo = repo;
            _messageBusClient = messageBusClient;
        }

        [Route("GetAllProducts")]
        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> GetProducts()
        {
            IEnumerable<Product> products = _repo.GetAllProducts();
            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<ProductReadDto> GetProductById(int id)
        {
            Product product = _repo.GetProductById(id:id);
            return Ok(_mapper.Map<ProductReadDto>(product));
        }

        [HttpPost]
        public ActionResult<ProductReadDto> CreateProduct(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);
            _repo.CreateProduct(product:product);
            _repo.SaveChanges();

            var productReadDto = _mapper.Map<ProductReadDto>(product);

            //Send Async Message
            try
            {
                var productPublishedDto = _mapper.Map<ProductPublishedDto>(productReadDto);
                productPublishedDto.Event = "Product_Published";
                _messageBusClient.PublishNewProduct(productPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetProductById), new { Id = productReadDto.Id }, productReadDto);
        }
    }
}
