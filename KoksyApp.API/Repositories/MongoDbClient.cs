using KoksyApp.API.Models;
using KoksyApp.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KoksyApp.API.Repositories;

public interface IMongoDbClient
{
    public IMongoDatabase GetKoksyDatabase();
}
public class MongoDbClient :IMongoDbClient
{
    private readonly DatabaseSettings _dbSettings;

    public MongoDbClient(IOptions<DatabaseSettings> configuration)
    {
        _dbSettings = configuration.Value;
    }
    public  IMongoDatabase GetKoksyDatabase()
    {
        
        var settings = MongoClientSettings.FromConnectionString("mongodb+srv://pkarys:Bhv5S6iRoTtcxzKs@pkcluster.xs5uz.mongodb.net/KoksyApp?retryWrites=true&w=majority");
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient("mongodb+srv://pkarys:Bhv5S6iRoTtcxzKs@pkcluster.xs5uz.mongodb.net/KoksyApp?retryWrites=true&w=majority");
        var database = client.GetDatabase("KoksyApp");

        client.ListDatabaseNames();
        var db = client.GetDatabase("KoksyApp");
        return db;
    }
}

public class BaseRepository<T>
{
    private readonly IMongoDbClient _mongoDbClient;
    protected  IMongoDatabase db;

    protected BaseRepository(IMongoDbClient mongoDbClient)
    {
        _mongoDbClient = mongoDbClient; 
        db = _mongoDbClient.GetKoksyDatabase();

    }

    protected IMongoCollection<T> GetCollection()
    {
        
        return db.GetCollection<T>("WorkoutDay");
    }
    
}