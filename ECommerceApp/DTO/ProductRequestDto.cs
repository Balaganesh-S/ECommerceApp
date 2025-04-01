using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.DTO
{
    public class ProductRequestDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
