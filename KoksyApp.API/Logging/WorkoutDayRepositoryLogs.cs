
using System;
using System.Threading.Tasks;
using KoksyApp.API.Models;
using KoksyApp.API.Repositories;
using Microsoft.Extensions.Logging;

namespace KoksyApp.API.Logging;

public class WorkoutDayRepositoryLogsDecorator :IWorkoutDayRepository
{
    private readonly IWorkoutDayRepository decorated;
    private readonly ILogger logger;

    public WorkoutDayRepositoryLogsDecorator(IWorkoutDayRepository decorated, ILogger<WorkoutDayRepository> logger)
    {
        this.decorated = decorated;
        this.logger = logger;
    }

    public WorkoutDay[] GetWorkoutDays()
    {
        try
        {
            logger.LogInformation("Getting data from database");
            var days = decorated.GetWorkoutDays();
            logger.LogInformation($"Days:{days.Length}");
            return days;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Exception in getting data from database");
            return Array.Empty<WorkoutDay>();
        }
        
    }

    public Task<WorkoutDay> GetWorkoutDay(string id)
    {
        try                                                                                 
        {                                                                                   
            logger.LogInformation("Getting data from database");                           
            return decorated.GetWorkoutDay(id);                                             
        }                                                                                   
        catch (Exception e)                                                                 
        {                                                                                   
            logger.LogError(e, "Exception in getting data from database");
            return null!;
        }
    }

    public void Add(WorkoutDay forCreation)
    {
        try                                                                                 
        {                                                                                   
            logger.LogInformation("Posting to database", forCreation); 
            decorated.Add(forCreation);                                             
        }                                                                                   
        catch (Exception e)                                                                 
        {                                                                                   
            logger.LogError(e, "Exception in adding data from database");                 
        }                                                                               
   }
}