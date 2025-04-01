using ECommerceApp.Domain;
using ECommerceApp.DTO;

namespace ECommerceApp.Services
{
    public interface ICartService
    {
        Task<CartDto> AddItemToCartAsync(int productId, int quantity);

        Task<Cart> GetUsersCart();
        Task<CartItem> UpdateItemQuantity(int productId, string operation);

        Task<CartItem> DeleteItemFromCart(int cartId, int itemId);

    }
}
