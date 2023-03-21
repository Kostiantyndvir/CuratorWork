namespace WebApplication1.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Kurator>? Kurators { get; set; }
    }
}
