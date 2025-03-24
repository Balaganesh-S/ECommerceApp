using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceApp.Domain
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey("Category")]
        public virtual int? CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category? Category { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    }
}
