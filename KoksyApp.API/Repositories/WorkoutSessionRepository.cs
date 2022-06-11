using KoksyApp.API.Models;
using MongoDB.Driver;

namespace KoksyApp.API.Repositories;

public interface IWorkoutSessionRepository
{
    public Task<bool> AddWorkoutSession(WorkoutSession session);
    public Task<WorkoutSession> GetLastSession(int id);

}

public class WorkoutSessionRepository :BaseRepository<WorkoutSession>, IWorkoutSessionRepository
{
    private readonly IMongoDbClient _mongoDbClient;

    public WorkoutSessionRepository(IMongoDbClient mongoDbClient) : base(mongoDbClient)
    {
        _mongoDbClient = mongoDbClient;
    }

    public Task<bool> AddWorkoutSession(WorkoutSession session)
    {
        var collection =  GetCollection();
        collection.InsertOne(session);
        return collection.Find(x => x.CreatedAt == session.CreatedAt).AnyAsync();
    }

    public Task<WorkoutSession> GetLastSession(int id)
    {
        var sorting = Builders<WorkoutSession>.Sort.Descending("CreatedAt");

        return  GetCollection()
            .Find(_ => true)
            .Sort(sorting)
            .FirstOrDefaultAsync();
    }

}