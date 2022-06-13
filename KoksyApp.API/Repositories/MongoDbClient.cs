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
        try
        {
            var settings = MongoClientSettings.FromConnectionString(
                _dbSettings.ConnectionString);
            settings.UseTls = true;
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client =
                new MongoClient(settings);
            var database = client.GetDatabase(_dbSettings.DatabaseName);
            return database;
        }
        catch (Exception e)
        {
            return null;
        }
       
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