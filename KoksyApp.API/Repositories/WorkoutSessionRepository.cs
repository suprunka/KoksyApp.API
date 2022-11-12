using System.Threading.Tasks;
using KoksyApp.API.Models;
using MongoDB.Driver;

namespace KoksyApp.API.Repositories;

public interface IWorkoutSessionRepository
{
    public Task<bool> AddWorkoutSession(WorkoutSession session);
    public Task<WorkoutSession?> GetLastSessions(string id, string userId, int setNumber);

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

    public Task<WorkoutSession> GetLastSessions(string workoutId, string userId, int setNumber)
    {
        var sorting = Builders<WorkoutSession>.Sort.Descending("CreatedAt");

        return GetCollection()
            .Find(_ => _.WorkoutId == workoutId && _.UserId == userId && _.SetNumber == setNumber)
            .Sort(sorting)
            .FirstOrDefaultAsync();
    }

}