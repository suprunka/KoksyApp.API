using KoksyApp.Dtos.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KoksyApp.API.Models;

public class WorkoutDay
{
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
    public Workout(string workoutDayId, int minReps, int maxReps, int sessionsCount, string name, string uri)
    {
        WorkoutDayId = workoutDayId;
        MinReps = minReps;
        MaxReps = maxReps;
        SessionsCount = sessionsCount;
        Name = name;
        Uri = uri;
    }
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string WorkoutDayId { get; set; }
    
    public int MinReps { get; set; }
    public int MaxReps { get; set; }    
    public int SessionsCount { get; set; }    
    public string Name { get; set; }
    public string Uri { get; }
    public string Url { get; set; }
    
    //public WorkoutSession LastSession { get; set; }

}
public class WorkoutSession
{
    public WorkoutSession(WorkoutSessionForCreation creation)
    {
        Reps = creation.Reps;
        Weight = creation.Weight;
        CreatedAt = DateTime.UtcNow;
        WorkoutId = creation.WorkoutId;
    }
    public int Reps { get; set; }
    public double Weight { get; set; }
    public DateTime CreatedAt { get; set; }
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }    
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string WorkoutId { get; set; }
    
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
