using ECommerceApp.Data;
using ECommerceApp.Domain;
using ECommerceApp.DTO;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceAppContext dbContext;
        public OrderRepository(ECommerceAppContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Order>> GetAllOrdersByUserEmail(string email)
        {
           List<Order> orders = await dbContext.orders
                                .Include(c=> c.OrderItems)
                                .Include(c=> c.Payment)
                                .Include(c=> c.Address)
                                .ToListAsync();
            return orders;
        }

        public async Task SaveOrderAsync(Order order)
        {
            Console.WriteLine($"OrderId before save: {order.Id}");  // Debugging
            await dbContext.orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
        }
    }
}
