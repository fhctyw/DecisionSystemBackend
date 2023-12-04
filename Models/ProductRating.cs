namespace DecisionSystem.Models
{
    public class ProductRating
    {
        public int Id { get; set; }
        public int ProductId {  get; set; }
        public int CustomerId { get; set; }
        public byte Rate { get; set; }
        public DateTime Date { get; set; }
    }

}
