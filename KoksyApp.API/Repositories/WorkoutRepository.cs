using KoksyApp.API.Models;
using MongoDB.Driver;

namespace KoksyApp.API.Repositories;

public interface IWorkoutRepository
{
    public Task<List<Workout>> GetWorkoutsForDay(int dayId);

}


public class WorkoutRepository :BaseRepository<Workout>, IWorkoutRepository
{
    public WorkoutRepository(IMongoDbClient mongoDbClient) : base(mongoDbClient)
    {
    }

    public Task<List<Workout>> GetWorkoutsForDay(int dayId)
    {
        return GetCollection().Find(_ => _.WorkoutDayId == dayId).ToListAsync();
    }
}