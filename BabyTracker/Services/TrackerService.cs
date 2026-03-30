using BabyTracker.models;
using BabyTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace BabyTracker.Services;

public class TrackerService{
private readonly BabyTrackerContext _context;

    public TrackerService(BabyTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<Tracker>> GetAllAsync()
    {
        return await _context.Trackers.ToListAsync();
    }

    public async Task<Tracker?> GetAsync(int Id)
    {
        return await _context.Trackers.FindAsync(Id);
    }

     public async Task<Tracker> AddAsync(Tracker tracker)
    {
        _context.Trackers.Add(tracker);
        await _context.SaveChangesAsync();
        return tracker;
    }

 public async Task<bool> UpdateAsync(Tracker tracker)
    {
        var existingTracker = await _context.Trackers.FindAsync(tracker.Id);
        if (existingTracker == null)
            return false;

        existingTracker.Milk = tracker.Milk;
        existingTracker.Food = tracker.Food;
        existingTracker.FoodAmount = tracker.FoodAmount;
        existingTracker.Poop = tracker.Poop;
      

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
        var tracker = await _context.Trackers.FindAsync(Id);
        if (tracker == null)
            return false;

        _context.Trackers.Remove(tracker);
        await _context.SaveChangesAsync();
        return true;
    }
}
 