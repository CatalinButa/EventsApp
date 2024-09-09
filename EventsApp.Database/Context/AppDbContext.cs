using EventsApp.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsApp.Database.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEvent>(entity =>
            {
                entity.HasKey(ue => new { ue.UserId, ue.EventId });

                entity.HasOne(ue => ue.Event)
                      .WithMany(e => e.Participants)
                      .HasForeignKey(ue => ue.EventId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ue => ue.User)
                      .WithMany()
                      .HasForeignKey(ue => ue.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
