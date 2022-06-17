using KoksyApp.API.Models;
using KoksyApp.API.Repositories;

namespace KoksyApp.API.Logging;

public class WorkoutSessionRepositoryLogs :IWorkoutSessionRepository
{
    private readonly ILogger<WorkoutSessionRepository> _logger;
    private readonly IWorkoutSessionRepository _decorated;

    public WorkoutSessionRepositoryLogs(ILogger<WorkoutSessionRepository> logger, IWorkoutSessionRepository decorated)
    {
        _logger = logger;
        _decorated = decorated;
    }

    public Task<bool> AddWorkoutSession(WorkoutSession session)
    {
        try                                                                                 
        {                                                                                   
            _logger.LogInformation("Posting to database", session); 
            return _decorated.AddWorkoutSession(session);                                             
        }                                                                                   
        catch (Exception e)                                                                 
        {                                                                                   
            _logger.LogError(e, "Exception in adding data to database", session);
            return Task.FromResult(false);
        }   
    }

    public Task<WorkoutSession> GetLastSession(Guid id)
    {
        try                                                                                 
        {                                                                                   
            _logger.LogInformation("Getting data from database, last session for id:", id); 
            return _decorated.GetLastSession(id);                                             
        }                                                                                   
        catch (Exception e)                                                                 
        {                                                                                   
            _logger.LogError(e, "Exception in adding data from database");
            return null!;
        }   
    }
}