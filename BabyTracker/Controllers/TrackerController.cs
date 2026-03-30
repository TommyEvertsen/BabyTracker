using BabyTracker.models;
using BabyTracker.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BabyTracker.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TrackerController : ControllerBase
{
    
     private readonly TrackerService _trackerService;

    public TrackerController(TrackerService trackerService)
    {
        _trackerService = trackerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Tracker>>> GetAll()
    {
        var trackers = await _trackerService.GetAllAsync();
        return Ok(trackers);
    }

    // GET by Id action
    [HttpGet("{Id}")]
    public async Task<ActionResult<Tracker>> Get(int Id)
    {
        var tracker = await _trackerService.GetAsync(Id);

        if(tracker == null)
            return NotFound();

        return tracker;
    }

    // POST action
    [HttpPost]
    public async Task<IActionResult> Create(Tracker tracker)
    {            
        var createdTracker = await _trackerService.AddAsync(tracker);
        return CreatedAtAction(nameof(Get), new { Id = createdTracker.Id }, createdTracker);
    }

    
    // PUT action
    [HttpPut("{Id}")]
    public async Task<IActionResult> Update(int Id, Tracker tracker)
    {
        if (Id != tracker.Id)
            return BadRequest();
           
        var updated = await _trackerService.UpdateAsync(tracker);
        if(!updated)
            return NotFound();
   
        return NoContent();
    }

    // DELETE action
    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        var deleted = await _trackerService.DeleteAsync(Id);
   
        if (!deleted)
            return NotFound();
       
        return NoContent();
    }

}