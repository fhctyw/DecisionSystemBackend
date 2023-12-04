namespace DecisionSystem.DTOs
{
    public class CartDTO
    {
        public int CustomerId { get; set; }
        public List<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();
    }
}
