using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public ProductController(IProductRepo repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
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

            var productReadDto = _mapper.Map<ProductReadDto>(productCreateDto);

            return CreatedAtRoute(nameof(GetProductById), new { Id = productReadDto.Id }, productReadDto);
        }
    }
}
