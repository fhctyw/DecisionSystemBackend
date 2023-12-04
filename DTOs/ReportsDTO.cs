namespace DecisionSystem.DTOs
{
    public class Analyses
    {
        public int ProductId {  get; set; }
        public decimal Popularity { get; set; }
    }

    public class ReportsDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Analyses> Analyses { get; set; }
    }
}
