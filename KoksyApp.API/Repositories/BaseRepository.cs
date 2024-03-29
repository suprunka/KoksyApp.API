﻿using MongoDB.Driver;

namespace KoksyApp.API.Repositories;

public class BaseRepository<T>
{
    private readonly IMongoDbClient mongoDbClient;
    public readonly IMongoDatabase db;

    protected BaseRepository(IMongoDbClient mongoDbClient)
    {
        this.mongoDbClient = mongoDbClient; 
        db = this.mongoDbClient.GetKoksyDatabase();
    }

    protected IMongoCollection<T> GetCollection()
    {
        
        var x=  db.GetCollection<T>(typeof(T).Name);
        return x;
    }
    
}