namespace DecisionSystem.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
    }
}