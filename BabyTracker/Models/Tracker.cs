using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabyTracker.models;

public class Tracker
{
    public int Id {get; set;}
    
    
    [Required]
    public int BabyId {get; set;}
    
  
    [ForeignKey("BabyId")]
    public Baby? Baby {get; set;}
    
    public int? Milk {get; set;}
    public string? Food {get; set;}
    public int? FoodAmount {get; set;}
    public bool? Poop {get; set;}


}