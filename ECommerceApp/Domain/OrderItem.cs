using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceApp.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
        public decimal OrderedProductPrice { get; set; }


    }
}
