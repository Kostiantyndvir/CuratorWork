namespace WebApplication1.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public string Interval { get; set; } = null!;
        public DateOnly Date { get; set; }
        public Group Group { get; set; } = null!;
    }
}
