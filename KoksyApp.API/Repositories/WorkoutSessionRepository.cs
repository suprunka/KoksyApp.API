using KoksyApp.API.Models;
using MongoDB.Driver;

namespace KoksyApp.API.Repositories;

public interface IWorkoutSessionRepository
{
    public Task<bool> AddWorkoutSession(WorkoutSession session);
    public Task<WorkoutSession> GetLastSession(string id, string userId);

}

public class WorkoutSessionRepository :BaseRepository<WorkoutSession>, IWorkoutSessionRepository
{
    private readonly IMongoDbClient mongoDbClient;

    public WorkoutSessionRepository(IMongoDbClient mongoDbClient) : base(mongoDbClient)
    {
        this.mongoDbClient = mongoDbClient;
    }

    public Task<bool> AddWorkoutSession(WorkoutSession session)
    {
        var collection =  GetCollection();
        collection.InsertOne(session);
        return collection.Find(x => x.CreatedAt == session.CreatedAt).AnyAsync();
    }

    public Task<WorkoutSession> GetLastSession(string id, string userId)
    {
        var sorting = Builders<WorkoutSession>.Sort.Descending("CreatedAt");

        return  GetCollection()
            .Find(_ => _.Id == id)
            .Sort(sorting)
            .FirstOrDefaultAsync();
    }

}