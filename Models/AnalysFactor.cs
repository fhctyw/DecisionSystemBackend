namespace DecisionSystem.Models
{
    public class AnalysFactor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public bool IsImplemented { get; set; }
    }
}
