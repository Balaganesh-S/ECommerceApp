using ECommerceApp.Domain;
using ECommerceApp.DTO;
using ECommerceApp.Repositories;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;
        private readonly ICartRepository cartRepository;

        public CartController(ICartService cartService, ICartRepository cartRepository)
        {
            this.cartService = cartService;
            this.cartRepository = cartRepository;
        }
        [Route("api/[controller]/products/{productId}/quantity/{quantity}")]
        [HttpPost]
        public async Task<CartDto> AddItemToCartAsync(int productId, int quantity) {
            return await cartService.AddItemToCartAsync(productId, quantity);
        }

        [Route("api/[controller]")]
        [HttpGet]
        public async Task<List<Cart>> GetCartsAsync()
        {
            return await cartRepository.GetCartsAsync();
        }

        [Route("api/[controller]/users/cart")]
        [HttpGet]
        public async Task<Cart> GetUsersCart()
        {
            return await cartService.GetUsersCart();
        }

        [Route("api/[controller]/products/{productId}/quantity/{operation}")] //operation can be "increase" or "decrease"
        [HttpPut]
        public async Task<CartItem> UpdateItemQuantity(int productId, string operation)
        {
            return await cartService.UpdateItemQuantity(productId, operation);
        }

        [Route("api/Carts/{cartId}/Items/{itemId}")]
        [HttpDelete]
        public async Task<CartItem> DeleteItemFromCart(int cartId, int itemId)
        {
            return await cartService.DeleteItemFromCart(cartId, itemId);
        }

    }
}
