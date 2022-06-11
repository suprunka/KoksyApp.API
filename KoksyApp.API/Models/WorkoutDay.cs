namespace KoksyApp.API.Models;

public class WorkoutDay
{
    public string Name { get; set; }
    public int Id { get; set; }
    public DateTime LastOpened { get; set; }
    public List<Workout> Workouts { get; set; }  
    
}
public class Workout
{
    public int WorkoutDayId { get; set; }
    public int Id { get; set; }
    public int MinReps { get; set; }
    public int MaxReps { get; set; }    
    public int SessionsCount { get; set; }    
    public string Name { get; set; }
    public string Url { get; set; }
    public WorkoutSession LastSession { get; set; }

}
public class WorkoutSession
{
    public WorkoutSession(WorkoutSessionForCreation creation)
    {
        Reps = creation.Reps;
        Weight = creation.Weight;
        CreatedAt = DateTime.UtcNow;
    }
    public int Reps { get; set; }
    public double Weight { get; set; }
    public DateTime CreatedAt { get; set; }
}
public class WorkoutSessionForCreation
{
    public int Reps { get; set; }
    public double Weight { get; set; }
}