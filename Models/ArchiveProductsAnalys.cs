namespace DecisionSystem.Models
{
    public class ArchiveProductsAnalys
    {
        public int Id { get; set; }
        public int AnalysReportId {  get; set; }
        public int ProductId { get; set; }
        public decimal Popularity { get; set; }
    }

}
