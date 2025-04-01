using ECommerceApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Data
{
    public class ECommerceAppContext : DbContext
    {
        public ECommerceAppContext(DbContextOptions<ECommerceAppContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> orders { get; set; }






    }
}
