namespace ECommerceApp.DTO
{
    public class CartItemDto
    {
        public int? Id { get; set; }

        public CartDto Cart { get; set; }
        public ProductDto Product { get; set; }

        public int ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
