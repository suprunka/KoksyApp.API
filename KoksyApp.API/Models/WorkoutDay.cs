using System;
using System.Collections.Generic;
using KoksyApp.Dtos.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KoksyApp.API.Models;

public class WorkoutDay
{
    public WorkoutDay(){}
    public WorkoutDay(string name)
    {
        Name = name;
        LastOpened = DateTime.UtcNow;
    }
    public string Name { get; set; }
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime LastOpened { get; set; }
    public List<Workout> Workouts { get; set; }  
    
}
public class Workout
{
    public Workout(string workoutDayId, int minReps, int maxReps, int sessionsCount, string name, string url, int breakSeconds)
    {
        WorkoutDayId = workoutDayId;
        MinReps = minReps;
        MaxReps = maxReps;
        SessionsCount = sessionsCount;
        Name = name;
        Url = url;
        BreakSeconds = breakSeconds;
    }
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
  
    [BsonRepresentation(BsonType.ObjectId)]
    public string WorkoutDayId { get; set; }
    
    public int MinReps { get; set; }
    public int MaxReps { get; set; }    
    public int SessionsCount { get; set; }    
    public string Name { get; set; }
    public string Url { get; set; }
    public int BreakSeconds { get; set; }
    
    //public WorkoutSession LastSession { get; set; }

    public BsonDocument ToBsonDocument()
    {
        return new BsonDocument()
        {
            {"WorkoutDayId", WorkoutDayId},
            {"MinReps", MinReps},
            {"MaxReps", MaxReps},
            {"SessionsCount", SessionsCount},
            {"Name", Name},
            {"Url", Url},
            {"BreakSeconds", BreakSeconds},
        };
    }
}
public class WorkoutSession
{
    public WorkoutSession()
    {
        
    }
    public WorkoutSession(int reps, int setNumber, double weight, string workoutId, string userId)
    {
        Reps =reps;
        Weight = weight;
        CreatedAt = DateTime.UtcNow;
        WorkoutId = workoutId;
        SetNumber =setNumber;
        UserId = userId;
    }

    public int SetNumber { get; set; }

    public int Reps { get; set; }
    public double Weight { get; set; }
    public DateTime CreatedAt { get; set; }
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }    
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string WorkoutId { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }
    
}



public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("Email")]
    public string Email { get; set; }
    
    [BsonElement("Password")]
    public string Password { get; set; }
    [BsonElement("Name")]
    public string Name { get; set; }
    
}
public class UserDay
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]
    public string DayId { get; set; }
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }
    
    public DateTime LastOpened { get; set; }
}

public class UserWorkoutSession
{
    public UserWorkoutSession(string sessionId, string userId)
    {
        SessionId = sessionId;
        UserId = userId;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string SessionId { get; set; }
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }
    
    public DateTime CreatedAt { get; set; }

}
