namespace BabyTracker.models;


public class Tracker
{
    public required Baby Baby {get; set;}
    public int Id {get; set;}
    public int BabyId {get; set;}
    public int? Milk {get; set;}
    public string? Food {get; set;}
    public int? FoodAmount {get; set;}
    public bool? Poop {get; set;}


}