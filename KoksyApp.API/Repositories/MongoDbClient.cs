using KoksyApp.API.Settings;
using MongoDB.Driver;

namespace KoksyApp.API.Repositories;

public interface IMongoDbClient
{
    public IMongoDatabase GetKoksyDatabase();
}
public class MongoDbClient :IMongoDbClient
{
    private readonly DatabaseSettings dbSettings;

    public MongoDbClient(DatabaseSettings configuration)
    {
        dbSettings = configuration;
    }
    public  IMongoDatabase GetKoksyDatabase()
    {
        try
        {
            var settings = MongoClientSettings.FromConnectionString(
                dbSettings.ConnectionString);
            settings.UseTls = true;
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client =
                new MongoClient(settings);
            var database = client.GetDatabase(dbSettings.DatabaseName);
            return database;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}