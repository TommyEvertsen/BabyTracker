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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Ensure database is created and BabyId column exists
        if (!optionsBuilder.IsConfigured) return;
        
        base.OnConfiguring(optionsBuilder);
    }

    public void EnsureDatabaseSchema()
    {
        try
        {
            // Try to execute a simple query to check if BabyId column exists
            Database.ExecuteSqlRaw("SELECT BabyId FROM Trackers LIMIT 1");
        }
        catch
        {
            // Column doesn't exist, so add it
            try
            {
                Database.ExecuteSqlRaw("INSERT OR IGNORE INTO Babies (Id, Name, Age) VALUES (1, 'DefaultBaby', 1)");
                Database.ExecuteSqlRaw("ALTER TABLE Trackers ADD COLUMN BabyId INTEGER DEFAULT 1");
                Database.ExecuteSqlRaw("UPDATE Trackers SET BabyId = 1 WHERE BabyId IS NULL OR BabyId = 0");
                Database.ExecuteSqlRaw("CREATE UNIQUE INDEX IF NOT EXISTS IX_Trackers_BabyId ON Trackers (BabyId)");
            }
            catch (Exception ex)
            {
                // Log the error but don't break the app
                Console.WriteLine($"Error adding BabyId column: {ex.Message}");
            }
        }
    }

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
            
          
            entity.HasOne(t => t.Baby)
                  .WithOne(b => b.Tracker)
                  .HasForeignKey<Tracker>(t => t.BabyId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}