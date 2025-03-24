using ECommerceApp.Domain;
using ECommerceApp.DTO;
using ECommerceApp.Repositories;
using ECommerceApp.Services;
using System.Security.Claims;

public class CartService : ICartService
{
    private readonly ICartRepository cartRepository;
    private readonly IProductRepository productRepository;
    private readonly IHttpContextAccessor httpContextAccessor;

    public CartService(ICartRepository cartRepository,
                       IProductRepository productRepository,
                       IHttpContextAccessor httpContextAccessor)
    {
        this.cartRepository = cartRepository;
        this.productRepository = productRepository;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<CartDto> AddItemToCartAsync(int productId, int quantity)
    {
        // Get logged-in user's email
        var userEmail = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(userEmail))
        {
            throw new UnauthorizedAccessException("User is not logged in.");
        }

        // Fetch user details using email
        var user = await userRepository.GetUserByEmailAsync(userEmail);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        // Fetch user cart using UserId
        var cart = await cartRepository.GetCartByUserIdAsync(user.Id);

        if (cart == null)
        {
            cart = new Cart { UserId = user.Id, Items = new List<CartItem>() };
        }

        // Fetch product details
        var product = await productRepository.GetProductByIdAsync(productId);
        if (product == null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        // Add item to cart
        var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (cartItem == null)
        {
            cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });
        }
        else
        {
            cartItem.Quantity += quantity;
        }

        // Save the cart
        await cartRepository.SaveCartAsync(cart);

        return new CartDto
        {
            UserId = user.Id,
            Items = cart.Items.Select(i => new CartItemDto { ProductId = i.ProductId, Quantity = i.Quantity }).ToList()
        };
    }

}
