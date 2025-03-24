using ECommerceApp.Domain;
using ECommerceApp.DTO;

namespace ECommerceApp.Repositories
{
    public interface ICartRepository
    {
        //Task<List<CartDto>> GetCartsAsync();
        Task<Cart> GetCartByUserEmailAsync(string email);
        Task SaveCartAsync(Cart cart);
        //Task<CartDto> GetCartAsync();
        //Task<CartDto> UpdateItemQuantity(int productId, string operation);
        //Task<CartDto> RemoveItemFromCartAsync(int cartId, int productId);
        //Task<CartDto> ClearCartAsync(int cartId);
    }
}
