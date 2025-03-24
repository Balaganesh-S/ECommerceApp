using AutoMapper;
using ECommerceApp.Domain;
using ECommerceApp.DTO;

namespace ECommerceApp.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
        }
    }
}
