using BabyTracker.models;
using Microsoft.EntityFrameworkCore;

namespace BabyTracker.Data;

public class BabyTrackerContext : DbContext
{
    public BabyTrackerContext(DbContextOptions<BabyTrackerContext> options) : base(options)
    {
    }

    public DbSet<Baby> Babies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baby>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Age).IsRequired();
        });
    }
}