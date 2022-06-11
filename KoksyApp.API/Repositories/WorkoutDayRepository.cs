using KoksyApp.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KoksyApp.API.Repositories;
public interface IWorkoutDayRepository
{
    public Task<List<WorkoutDay>> GetWorkoutDays();
}

public class WorkoutDayRepository :BaseRepository<WorkoutDay>, IWorkoutDayRepository
{
    public WorkoutDayRepository(IMongoDbClient mongoDbClient) : base(mongoDbClient)
    {
    }

    public async Task<List<WorkoutDay>> GetWorkoutDays()
    {
        var dbList = db.ListCollections().ToList();

        var collection = GetCollection();
        var document = new WorkoutDay()
        {
            Name = "dldjkd"
        };
        collection.InsertOne(document);
        return new List<WorkoutDay>();
    }
}