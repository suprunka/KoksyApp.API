using MongoDB.Driver;

namespace KoksyApp.API.Repositories;

public class BaseRepository<T>
{
    private readonly IMongoDbClient mongoDbClient;
    private readonly IMongoDatabase db;

    protected BaseRepository(IMongoDbClient mongoDbClient)
    {
        this.mongoDbClient = mongoDbClient; 
        db = this.mongoDbClient.GetKoksyDatabase();
    }

    protected IMongoCollection<T> GetCollection()
    {
        return db.GetCollection<T>(typeof(T).Name);
    }
    
}