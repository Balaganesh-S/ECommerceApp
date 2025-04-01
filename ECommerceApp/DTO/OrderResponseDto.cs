using ECommerceApp.Domain;
using System.Text.Json.Serialization;

namespace ECommerceApp.DTO
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<OrderItemResponseDto> OrderItems { get; set; } = new List<OrderItemResponseDto>();
        public DateTime OrderDate { get; set; }
        public virtual PaymentResponseDto PaymentResponse { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public virtual AddressResponseDto AddressResponseDto { get; set; }
    }
}
