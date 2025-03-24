namespace ECommerceApp.DTO
{
    public class CartDto
    {
        public int? Id { get; set; }
        public double TotalPrice { get; set; } = 0.0;
        public List<CartItemDto> CartItems { get; set; }
    }
}
