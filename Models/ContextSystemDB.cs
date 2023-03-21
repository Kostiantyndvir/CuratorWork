using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class ContextSystemDB : DbContext
    {
        public DbSet<Report> Reports { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Kurator> Kurators { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public ContextSystemDB(DbContextOptions<ContextSystemDB> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
