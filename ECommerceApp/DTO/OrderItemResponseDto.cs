using ECommerceApp.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceApp.DTO
{
    public class OrderItemResponseDto
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal OrderedProductPrice { get; set; }
    }
}
