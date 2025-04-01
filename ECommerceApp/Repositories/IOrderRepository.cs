using ECommerceApp.Domain;
using ECommerceApp.DTO;

namespace ECommerceApp.Repositories
{
    public interface IOrderRepository
    {
        public Task SaveOrderAsync(Order order);
        public Task<List<Order>> GetAllOrdersByUserEmail(string email);
    }
}
