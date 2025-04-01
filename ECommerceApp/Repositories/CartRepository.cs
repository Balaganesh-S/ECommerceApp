using ECommerceApp.Data;
using ECommerceApp.Domain;
using ECommerceApp.DTO;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace ECommerceApp.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ECommerceAppContext dbContext;

        public CartRepository(ECommerceAppContext dbContext)
        {
            this.dbContext= dbContext;
        }

        public async Task ClearCartAsync(string userEmail)
        {
            var cart = await dbContext.Carts
                .Include(c => c.Items)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.User.Email == userEmail);

            if (cart != null)
            {
                dbContext.CartItems.RemoveRange(cart.Items); // Remove all items from the cart
                await dbContext.SaveChangesAsync(); // Save the changes
            }
        }


        public async Task<Cart> GetCartByIdAsync(int cartId)
        { 


            return await dbContext.Carts
                .Include(c => c.Items) // Load related cart items
                .FirstOrDefaultAsync(c => c.Id == cartId);
        }

        public async Task<Cart> GetCartByUserEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }

            return await dbContext.Carts
                .Include(c => c.Items) // Load related cart items
                .FirstOrDefaultAsync(c => c.User.Email == email);
        }

        

        public Task<List<Cart>> GetCartsAsync()
        {
            return dbContext.Carts
                .Include(c => c.Items) 
                .ToListAsync();
        }

        public async Task SaveCartAsync(Cart cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart), "Cart cannot be null.");
            }

            var existingCart = await dbContext.Carts
                .Include(c => c.Items) // Load cart items
                .FirstOrDefaultAsync(c => c.UserId == cart.UserId);

            if (existingCart != null)
            {
                // Update existing cart
                existingCart.Items = cart.Items; // Update the items list
                dbContext.Carts.Update(existingCart);
            }
            else
            {
                // Add new cart
                await dbContext.Carts.AddAsync(cart);
            }

            await dbContext.SaveChangesAsync();
        }

    }
}
