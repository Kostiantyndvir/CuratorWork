namespace WebApplication1.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Parents { get; set; }
        public string? Phone { get; set; }
        public string? ParentPhone { get; set; }
        public string? AddressHome { get; set; }
        public string? AddressStudy { get; set; }
        public string? Activities { get; set; }
        public Group Group { get; set; } = null!;
    }
}
