using KoksyApp.API.Models;

namespace KoksyApp.API.Repositories;


public interface IUserWorkoutSessionRepository
{
    bool CreateUserWorkout(UserWorkoutSession session);
}
public class UserWorkoutSessionRepository : BaseRepository<UserWorkoutSession>, IUserWorkoutSessionRepository
{
    protected UserWorkoutSessionRepository(IMongoDbClient mongoDbClient) : base(mongoDbClient)
    {
    }
    
    public bool CreateUserWorkout(UserWorkoutSession session)
    {
        session.CreatedAt = DateTime.UtcNow;
        
        GetCollection().InsertOne(session);
        return true;
    }

}