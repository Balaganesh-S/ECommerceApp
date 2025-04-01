using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceApp.Domain
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public DateTime OrderDate { get; set; }
        [JsonIgnore]
        public virtual Payment Payment { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }  
        public virtual Address Address { get; set; }
    }
}
