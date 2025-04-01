using ECommerceApp.Data;
using ECommerceApp.Domain;

namespace ECommerceApp.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ECommerceAppContext dbContext;
        public OrderItemRepository(ECommerceAppContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SaveOrderItemAsync(OrderItem orderItem)
        {
            await dbContext.OrderItems.AddAsync(orderItem);
            await dbContext.SaveChangesAsync();
        }
    }
}
