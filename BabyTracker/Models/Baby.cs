using System.ComponentModel.DataAnnotations;

namespace BabyTracker.models;

public class Baby
{
    public int Id {get; set;}
    
    [Required]
    public required string Name {get; set;}
    public required int Age {get; set;}
    

    public Tracker? Tracker {get; set;}
}