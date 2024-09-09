using EventsApp.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsApp.Database.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
