using ECommerceApp.Repositories;

namespace ECommerceApp.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository orderItemRepository;
        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            this.orderItemRepository = orderItemRepository;
        }
    }
}
