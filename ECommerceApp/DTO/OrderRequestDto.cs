namespace ECommerceApp.DTO
{
    public class OrderRequestDto
    {
        public int AddressId { get; set; }
        public string PGPaymentId { get; set; }
        public string PGStatus { get; set; }
        public string PGResponseMessage { get; set; }
        public string PGName { get; set; }
    }
}
