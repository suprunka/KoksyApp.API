using KoksyApp.API.Models;
using KoksyApp.API.Repositories;

namespace KoksyApp.API.Logging;

public class WorkoutSessionRepositoryLogs :IWorkoutSessionRepository
{
    private readonly ILogger<WorkoutSessionRepository> logger;
    private readonly IWorkoutSessionRepository decorated;

    public WorkoutSessionRepositoryLogs(ILogger<WorkoutSessionRepository> logger, IWorkoutSessionRepository decorated)
    {
        this.logger = logger;
        this.decorated = decorated;
    }

    public Task<bool> AddWorkoutSession(WorkoutSession session)
    {
        try                                                                                 
        {                                                                                   
            logger.LogInformation("Posting to database", session); 
            return decorated.AddWorkoutSession(session);                                             
        }                                                                                   
        catch (Exception e)                                                                 
        {                                                                                   
            logger.LogError(e, "Exception in adding data to database", session);
            return Task.FromResult(false);
        }   
    }

    public Task<WorkoutSession> GetLastSession(string id, string userId)
    {
        try                                                                                 
        {                                                                                   
            logger.LogInformation("Getting data from database, last session for id:", id); 
            return decorated.GetLastSession(id, userId);                                             
        }                                                                                   
        catch (Exception e)                                                                 
        {                                                                                   
            logger.LogError(e, "Exception in adding data from database");
            return null!;
        }   
    }
}