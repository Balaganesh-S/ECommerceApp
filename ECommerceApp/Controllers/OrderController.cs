using ECommerceApp.DTO;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService) {
            this.orderService = orderService;
        }
        [HttpPost]
        [Route("/order/users/payments/{paymentMethod}")]
        public async Task<IActionResult> OrderProducts([FromRoute] string paymentMethod, [FromBody] OrderRequestDto orderRequestDto)
        {
            var response = await orderService.PlaceOrder(
                orderRequestDto.AddressId,
                paymentMethod,
                orderRequestDto.PGName,
                orderRequestDto.PGPaymentId,
                orderRequestDto.PGStatus,
                orderRequestDto.PGResponseMessage);
            return Ok(response);
            
        }

        [HttpGet]
        [Route("/orders/users")]
        public async Task<ResponseDto<List<OrderResponseDto>>> GetAllOrdersByUser()
        {
            var response = await orderService.GetAllOrdersByUser();
            return response;
        }

    }
}
