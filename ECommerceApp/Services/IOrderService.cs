using ECommerceApp.DTO;

namespace ECommerceApp.Services
{
    public interface IOrderService
    {
        Task<ResponseDto<OrderResponseDto>> PlaceOrder(int addressId, string paymentMethod, string pGName, string pGPaymentId, string pGStatus, string pGResponseMessage);
        Task<ResponseDto<List<OrderResponseDto>>> GetAllOrdersByUser();
    }
}
