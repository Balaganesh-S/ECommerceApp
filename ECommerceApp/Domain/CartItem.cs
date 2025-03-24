using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceApp.Domain
{
    public class CartItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public double ProductPrice { get; set; }
        [ForeignKey("Product")]
        public virtual int? ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
        [ForeignKey("Cart")]
        public virtual int? CartId { get; set; }
        [JsonIgnore]
        public virtual Cart Cart { get; set; }
    }
}
