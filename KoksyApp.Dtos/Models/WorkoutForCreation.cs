namespace KoksyApp.Dtos.Models;

public class WorkoutForCreation
{
    public string WorkoutDayId { get; set; }
    public int MinReps { get; set; }
    public int MaxReps { get; set; }    
    public int SessionsCount { get; set; }    
    public string Name { get; set; }
    public Uri Url { get; set; }
}

public class WorkoutDayForCreation
{
    public string Name { get; set; }  
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