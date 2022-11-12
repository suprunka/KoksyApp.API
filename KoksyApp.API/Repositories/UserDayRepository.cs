using KoksyApp.API.Models;
using MongoDB.Driver;

namespace KoksyApp.API.Repositories;

public interface IUserDayRepository
{

    Task<bool> Add(string dayId, string userId);
    Task<UserDay[]> GetForUser(string userId);
    Task<UserDay> GetUserDay(string userId, string dayId);
    Task<UserDay> Update(string dayId, string userId, DateTime dateTime);
}
public class UserDayRepository :BaseRepository<UserDay>, IUserDayRepository
{
    private readonly IMongoDbClient mongoDbClient;

    public UserDayRepository(IMongoDbClient mongoDbClient) : base(mongoDbClient)
    {
        this.mongoDbClient = mongoDbClient;
    }


    public Task<bool> Add(string dayId, string userId)
    {
        GetCollection().InsertOne(new UserDay()
        {
            DayId = dayId,
            UserId = userId,
            LastOpened = DateTime.Now,
        });
        return  GetCollection().Find(x => x.UserId == userId && x.DayId == dayId).AnyAsync();
    }

    public async Task<UserDay[]> GetForUser(string userId)
    {
        return  GetCollection().Find(x => x.UserId == userId).ToEnumerable().ToArray();
    }

    public async Task<UserDay> GetUserDay(string userId, string dayId)
    {
        return  GetCollection().Find(x => x.UserId == userId && x.DayId == dayId).FirstOrDefault();
    }

    public Task<UserDay> Update(string dayId, string userId, DateTime dateTime)
    {
        var updatedef = Builders<UserDay>.Update
            .Set(x => x.LastOpened, dateTime);
      return  GetCollection().FindOneAndUpdateAsync(x => x.UserId == userId && x.DayId == dayId, updatedef);
    }
}