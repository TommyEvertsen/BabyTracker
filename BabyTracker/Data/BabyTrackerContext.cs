using BabyTracker.models;
using Microsoft.EntityFrameworkCore;

namespace BabyTracker.Data;

public class BabyTrackerContext : DbContext
{
    public BabyTrackerContext(DbContextOptions<BabyTrackerContext> options) : base(options)
    {
    }

    public DbSet<Baby> Babies { get; set; }

    public DbSet<Tracker> Trackers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baby>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Age).IsRequired();
        });

         modelBuilder.Entity<Tracker>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Food);
            entity.Property(e => e.FoodAmount);
            entity.Property(e => e.Milk);
            entity.Property(e => e.Poop);
        });
    }
}