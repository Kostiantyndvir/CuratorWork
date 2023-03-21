namespace WebApplication1.Models
{
    public class Kurator
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Department Department { get; set; } = null!;
        public List<Group>? Groups { get; set; }
    }
}
