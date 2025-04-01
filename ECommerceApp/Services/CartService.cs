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
    private readonly IUserRepository userRepository;

    public CartService(ICartRepository cartRepository,
                       IProductRepository productRepository,
                       IHttpContextAccessor httpContextAccessor,
                       IUserRepository userRepository)
    {
        this.cartRepository = cartRepository;
        this.productRepository = productRepository;
        this.httpContextAccessor = httpContextAccessor;
        this.userRepository = userRepository;
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
        var cart = await cartRepository.GetCartByUserEmailAsync(user.Email);

        if (cart == null)
        {
            cart = new Cart { UserId = user.Id, User = user};
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
            if (quantity> await productRepository.GetProductQuantityByID(productId))
            {
                throw new Exception("Quantity greater than Product Quantity");
            }
            cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity, ProductPrice = (double)product.Price, CartId = cart.Id, Cart = cart });
        }
        else
        {
            if ((cartItem.Quantity + quantity) > await productRepository.GetProductQuantityByID(productId))
            {
                throw new Exception("Quantity greater than Product Quantity");
            }
            cartItem.Quantity += quantity;
        }
        cart.TotalPrice += (double)product.Price * quantity;

        // Save the cart
        await cartRepository.SaveCartAsync(cart);

        return new CartDto
        {
            Id = cart.Id,
            TotalPrice = cart.TotalPrice,
            CartItems = cart.Items.Select(i => new CartItemDto
            {
                Id = i.Id,
                Quantity = i.Quantity,
                ProductPrice = (int)i.ProductPrice,
            }).ToList()
        };
    }

    public async Task<CartItem> DeleteItemFromCart(int cartId, int itemId)
    {
        Cart cart = await cartRepository.GetCartByIdAsync(cartId);
        
        CartItem cartItem = cart.Items.FirstOrDefault(i => i.Id == itemId) ?? throw new KeyNotFoundException("Item not found in cart.");

        // Update total price before removing the item
        cart.TotalPrice -= cartItem.Quantity * cartItem.ProductPrice;

        cart.Items.Remove(cartItem);
        await cartRepository.SaveCartAsync(cart);

        return cartItem;
    }


    public async Task<Cart> GetUsersCart()
    {
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
        var cart = await cartRepository.GetCartByUserEmailAsync(user.Email);

        if (cart == null)
        {
            throw new KeyNotFoundException("Cart not found.");
        }
        return cart;
    }

    public async Task<CartItem>  UpdateItemQuantity(int productId, string operation)
    {
        Cart cart = await GetUsersCart();
        CartItem cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (cartItem == null)
        {
            throw new KeyNotFoundException("Item not found in cart.");
        }
        cartItem.Quantity = operation == "increase" ? cartItem.Quantity + 1 : cartItem.Quantity - 1;
        await cartRepository.SaveCartAsync(cart);
        return cartItem;
    }

    
}
