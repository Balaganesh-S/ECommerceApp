using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain
{
    public class Payment
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public string PaymentMethod { get; set; }
        public string PGPaymentId { get; set; }
        public string PGStatus { get; set; }
        public string PGResponseMessage { get; set; }
        public string PGName { get; set; }
    }
}
