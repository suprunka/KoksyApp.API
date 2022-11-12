namespace KoksyApp.Dtos.Models;

public class WorkoutSessionForCreation
{
    public int Reps { get; set; }
    public double Weight { get; set; }
    public string WorkoutId { get; set; }
    public int SetNumber { get; set; }
}