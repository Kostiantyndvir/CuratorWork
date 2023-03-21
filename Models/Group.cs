namespace WebApplication1.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Kurator Kurator { get; set; } = null!;
        public List<Student> Students { get; set; } = new();
        public List<Report> Reports { get; set; } = new();
        public List<Event> Events { get; set; } = new();
    }
}
