using KoksyApp.API.Models;
using KoksyApp.API.Models;
using MongoDB.Driver;

namespace KoksyApp.API.Repositories;

public interface IUserRepository
{
    Task<bool> Add(User user);
    Task<User> Get(string id);
    User[] Get();
}

public class UserRepository :BaseRepository<User>, IUserRepository
{
    private readonly IMongoDbClient mongoDbClient;

    public UserRepository(IMongoDbClient mongoDbClient) : base(mongoDbClient)
    {
        this.mongoDbClient = mongoDbClient;
    }

    public Task<bool> Add(User user)
    {
        var collection =  GetCollection();
        collection.InsertOne(user);
        return collection.Find(x => x.Email == user.Email).AnyAsync();
    }

    public Task<User> Get(string id)
    {
        return  GetCollection()
            .Find(_ => _.Id == id)
            .FirstOrDefaultAsync();
    }

    public User[] Get()
    {
        return  GetCollection().AsQueryable().ToArray();
    }
}