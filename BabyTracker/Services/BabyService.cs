using BabyTracker.models;
using BabyTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace BabyTracker.Services;

public class BabyService
{
    private readonly BabyTrackerContext _context;

    public BabyService(BabyTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<Baby>> GetAllAsync()
    {
        return await _context.Babies.ToListAsync();
    }

    public async Task<Baby?> GetAsync(int Id)
    {
        return await _context.Babies.FindAsync(Id);
    }

    public async Task<Baby> AddAsync(Baby baby)
    {
        _context.Babies.Add(baby);
        await _context.SaveChangesAsync();
        return baby;
    }

    public async Task<bool> UpdateAsync(Baby baby)
    {
        var existingBaby = await _context.Babies.FindAsync(baby.Id);
        if (existingBaby == null)
            return false;

        existingBaby.Name = baby.Name;
        existingBaby.Age = baby.Age;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
        var baby = await _context.Babies.FindAsync(Id);
        if (baby == null)
            return false;

        _context.Babies.Remove(baby);
        await _context.SaveChangesAsync();
        return true;
    }
}