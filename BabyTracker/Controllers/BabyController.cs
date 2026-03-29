using BabyTracker.models;
using BabyTracker.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BabyTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BabyController : ControllerBase
{
    private readonly BabyService _babyService;

    public BabyController(BabyService babyService)
    {
        _babyService = babyService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Baby>>> GetAll()
    {
        var babies = await _babyService.GetAllAsync();
        return Ok(babies);
    }

    // GET by Id action
    [HttpGet("{Id}")]
    public async Task<ActionResult<Baby>> Get(int Id)
    {
        var baby = await _babyService.GetAsync(Id);

        if(baby == null)
            return NotFound();

        return baby;
    }

    // POST action
    [HttpPost]
    public async Task<IActionResult> Create(Baby baby)
    {            
        var createdBaby = await _babyService.AddAsync(baby);
        return CreatedAtAction(nameof(Get), new { Id = createdBaby.Id }, createdBaby);
    }

    
    // PUT action
    [HttpPut("{Id}")]
    public async Task<IActionResult> Update(int Id, Baby baby)
    {
        if (Id != baby.Id)
            return BadRequest();
           
        var updated = await _babyService.UpdateAsync(baby);
        if(!updated)
            return NotFound();
   
        return NoContent();
    }

    // DELETE action
    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        var deleted = await _babyService.DeleteAsync(Id);
   
        if (!deleted)
            return NotFound();
       
        return NoContent();
    }
}