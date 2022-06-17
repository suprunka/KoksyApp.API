using KoksyApp.API.Models;
using KoksyApp.API.Repositories;

namespace KoksyApp.API.Logging;

public class WorkoutRepositoryLogs :IWorkoutRepository
{
    private readonly ILogger<WorkoutRepository> _logger;
    private readonly IWorkoutRepository _decorated;

    public WorkoutRepositoryLogs(ILogger<WorkoutRepository> logger, IWorkoutRepository decorated)
    {
        _logger = logger;
        _decorated = decorated;
    }

    public Workout[] GetWorkoutsForDay(string dayId)
    {
        try                                                                                 
        {                                                                                   
            _logger.LogInformation($"Getting data from database for day= {dayId}"); 
            return _decorated.GetWorkoutsForDay(dayId);                                             
        }                                                                                   
        catch (Exception e)                                                                 
        {                                                                                   
            _logger.LogError(e, "Exception in adding data from database");
            return Array.Empty<Workout>();
        }           
    }

    public async Task AddWorkout(Workout workout)
    {
        try                                                                                 
        {                                                                                   
            _logger.LogInformation("Posting to database", workout); 
            await _decorated.AddWorkout(workout);                                             
        }                                                                                   
        catch (Exception e)                                                                 
        {                                                                                   
            _logger.LogError(e, "Exception in adding data from database");                 
        }   
    }
}