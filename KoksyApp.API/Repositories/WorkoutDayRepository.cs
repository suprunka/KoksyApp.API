using System;
using System.Linq;
using System.Threading.Tasks;
using KoksyApp.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KoksyApp.API.Repositories;
public interface IWorkoutDayRepository
{
    public WorkoutDay[] GetWorkoutDays();
    Task<WorkoutDay> GetWorkoutDay(string id);
    void Add(WorkoutDay forCreation);
}

public class WorkoutDayRepository :BaseRepository<WorkoutDay>, IWorkoutDayRepository
{
    public WorkoutDayRepository(IMongoDbClient mongoDbClient) : base(mongoDbClient)
    {
    }

    public WorkoutDay[] GetWorkoutDays()
    {
        try
        {
            return GetCollection().AsQueryable().ToArray();
        }
        catch (Exception e)
        {
            return Array.Empty<WorkoutDay>();

        }
    }

    public Task<WorkoutDay> GetWorkoutDay(string id)
    {
        return GetCollection().Find(c => c.Id == id).FirstOrDefaultAsync();
    }

    public void Add(WorkoutDay forCreation)
    {
        GetCollection().InsertOne(forCreation);
    }
}