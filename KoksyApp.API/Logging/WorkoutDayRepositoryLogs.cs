
using KoksyApp.API.Models;
using KoksyApp.API.Repositories;

namespace KoksyApp.API.Logging;

public class WorkoutDayRepositoryLogsDecorator :IWorkoutDayRepository
{
    private readonly IWorkoutDayRepository _decorated;
    private readonly ILogger _logger;

    public WorkoutDayRepositoryLogsDecorator(IWorkoutDayRepository decorated, ILogger<WorkoutDayRepository> logger)
    {
        this._decorated = decorated;
        _logger = logger;
    }

    public WorkoutDay[] GetWorkoutDays()
    {
        try
        {
            _logger.LogInformation("Getting data from database");
            return _decorated.GetWorkoutDays();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception in getting data from database");
            return Array.Empty<WorkoutDay>();
        }
        
    }

    public Task<WorkoutDay> GetWorkoutDay(string id)
    {
        try                                                                                 
        {                                                                                   
            _logger.LogInformation("Getting data from database");                           
            return _decorated.GetWorkoutDay(id);                                             
        }                                                                                   
        catch (Exception e)                                                                 
        {                                                                                   
            _logger.LogError(e, "Exception in getting data from database");
            return null!;
        }
    }

    public void Add(WorkoutDay forCreation)
    {
        try                                                                                 
        {                                                                                   
            _logger.LogInformation("Posting to database", forCreation); 
            _decorated.Add(forCreation);                                             
        }                                                                                   
        catch (Exception e)                                                                 
        {                                                                                   
            _logger.LogError(e, "Exception in adding data from database");                 
        }                                                                               
   }
}