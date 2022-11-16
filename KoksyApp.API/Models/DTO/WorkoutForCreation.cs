using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KoksyApp.API.Models.DTO;

public class WorkoutForCreation
{
    public string WorkoutDayId { get; set; }
    public int MinReps { get; set; }
    public int MaxReps { get; set; }    
    public int SessionsCount { get; set; }    
    public string Name { get; set; }
    public Uri Url { get; set; }
    public int BreakSeconds { get; set; }
}

public class WorkoutDayForCreation
{
    public string Name { get; set; }  
    public string Type { get; set; }  
}
public class WorkoutDayDto
{
    public string Name { get; set; }
    public string Id { get; set; }
    public DateTime LastOpened { get; set; }
    public List<WorkoutDto> Workouts { get; set; }  
}
public class WorkoutDto
{ 
}

public class UserForCreation
{
    public string Name { get; set; }  
    [Required]
    [EmailAddress]
    public string Email { get; set; }  
    [MinLength(8)]
    [Required]
    public string Password { get; set; }  
}
public class UserForLogin
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }  
    [MinLength(8)]
    [Required]
    public string Password { get; set; }  
}