using KoksyApp.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KoksyApp.API.Repositories;

public interface IWorkoutRepository
{
    public Workout[] GetWorkoutsForDay(string dayId);
    public Workout[] GetWorkoutsForUserDay(string dayId, string userId);
    public Task<Workout> GetWorkout(string id);
    public Task AddWorkout(Workout workout);

}


public class WorkoutRepository :BaseRepository<Workout>, IWorkoutRepository
{
    public WorkoutRepository(IMongoDbClient mongoDbClient) : base(mongoDbClient)
    {
    }

    public Workout[] GetWorkoutsForDay(string dayId)
    {
        return GetCollection().Find(_ => _.WorkoutDayId == dayId).ToEnumerable().ToArray();
    }
    public Workout[] GetWorkoutsForUserDay(string dayId, string userId)
    {
        return GetCollection().Find(_ => _.WorkoutDayId == dayId).ToEnumerable().ToArray();
    }

    public Task<Workout> GetWorkout(string id)
    {
        return GetCollection().Find(_ => _.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddWorkout(Workout workout)
    {
        var workoutsCollection = GetCollection();
        await workoutsCollection.InsertOneAsync(workout);
       
        //TODO:DECORATOR PATTERN
    }
}