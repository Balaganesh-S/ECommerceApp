using ECommerceApp.Domain;

namespace ECommerceApp.Repositories
{
    public interface IOrderItemRepository
    {
        public Task SaveOrderItemAsync(OrderItem orderItem);
    }
}
