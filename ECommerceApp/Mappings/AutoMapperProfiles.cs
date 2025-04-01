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
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressResponseDto>();
            CreateMap<Payment, PaymentResponseDto>();
            CreateMap<OrderItem, OrderItemResponseDto>();
            CreateMap<Order, OrderResponseDto>();
            CreateMap<Category, CategoryResponseDto>();
            CreateMap<CategoryRequestDto, Category>();
            CreateMap<ProductRequestDto, Product>();
            CreateMap<Product, ProductResponseDto>();
        }
    }
}
