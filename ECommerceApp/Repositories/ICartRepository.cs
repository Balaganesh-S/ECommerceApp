using ECommerceApp.Domain;
using ECommerceApp.DTO;

namespace ECommerceApp.Repositories
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetCartsAsync();
        Task<Cart> GetCartByUserEmailAsync(string email);

        Task<Cart> GetCartByIdAsync(int cartId);



        Task SaveCartAsync(Cart cart);
        //Task<CartDto> GetCartAsync();
        //Task<CartDto> UpdateItemQuantity(int productId, string operation);
        //Task<CartDto> RemoveItemFromCartAsync(int cartId, int productId);
        Task ClearCartAsync(string userEmail);
    }
}
