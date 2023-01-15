using AutoMapper;
using ProductService.Dtos;
using ProductService.Models;

namespace ProductService.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductEditDto, Product>();
            CreateMap<ProductReadDto, ProductPublishedDto>();
        }
    }
}
