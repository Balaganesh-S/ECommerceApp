using ECommerceApp.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.DTO
{
    public class PaymentResponseDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PGPaymentId { get; set; }
        public string PGStatus { get; set; }
        public string PGResponseMessage { get; set; }
        public string PGName { get; set; }
    }
}
