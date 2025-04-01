namespace ECommerceApp.DTO
{
    public class PaymentRequestDto
    {
        public string PaymentMethod { get; set; }
        public string PGPaymentId { get; set; }
        public string PGStatus { get; set; }
        public string PGResponseMessage { get; set; }
        public string PGName { get; set; }
    }
}
