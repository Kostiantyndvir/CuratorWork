namespace WebApplication1.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Group Group { get; set; } = null!;
        public DateOnly Date { get; set; }
    }
}
