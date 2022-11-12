using System;
using System.Threading.Tasks;
using KoksyApp.API.Models;
using KoksyApp.API.Repositories;
using Microsoft.Extensions.Logging;

namespace KoksyApp.API.Logging;

public class WorkoutRepositoryLogs :IWorkoutRepository
{
    private readonly ILogger<WorkoutRepository> logger;
    private readonly IWorkoutRepository decorated;

    public WorkoutRepositoryLogs(ILogger<WorkoutRepository> logger, IWorkoutRepository decorated)
    {
        this.logger = logger;
        this.decorated = decorated;
    }

    public Workout[] GetWorkoutsForDay(string dayId)
    {
        try                                                                                 
        {                                                                                   
            logger.LogInformation($"Getting data from database for day= {dayId}"); 
            return decorated.GetWorkoutsForDay(dayId);                                             
        }                                                                                   
        catch (Exception e)                                                                 
        {                                                                                   
            logger.LogError(e, "Exception in adding data from database");
            return Array.Empty<Workout>();
        }           
    }

    public Workout[] GetWorkoutsForUserDay(string dayId, string userId)
    {
        try                                                                                 
        {                                                                                   
            logger.LogInformation("Getting data from database for day {@dayId}, user: {@userId}", dayId, userId); 
            return decorated.GetWorkoutsForDay(dayId);                                             
        }                                                                                   
        catch (Exception e)                                                                 
        {                                                                                   
            logger.LogError(e, "Exception in adding data from database");
            return Array.Empty<Workout>();
        }       }

    public Task<Workout> GetWorkout(string id)
    {
        try                                                                                 
        {                                                                                   
            logger.LogInformation($@"Getting data from database for id= {id}"); 
            return decorated.GetWorkout(id);                                             
        }                                                                                   
        catch (Exception e)                                                                 
        {                                                                                   
            logger.LogError(e, "Exception in adding data from database");
            return null;
        }    
    }

    public async Task AddWorkout(Workout workout)
    {
        try                                                                                 
        {                                                                                   
            logger.LogInformation("Posting to database", workout); 
            await decorated.AddWorkout(workout);                                             
        }                                                                                   
        catch (Exception e)                                                                 
        {                                                                                   
            logger.LogError(e, "Exception in adding data from database");                 
        }   
    }
}