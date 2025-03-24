using ECommerceApp.Domain;
using ECommerceApp.DTO;

namespace ECommerceApp.Services
{
    public interface ICartService
    {
        Task<CartDto> AddItemToCartAsync(int productId, int quantity);
    }
}
