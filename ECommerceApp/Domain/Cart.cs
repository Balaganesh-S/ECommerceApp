using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceApp.Domain
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public virtual int? UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }

        public double TotalPrice { get; set; } = 0.0;

        public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
