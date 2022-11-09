using KoksyApp.API.Models;
using KoksyApp.API.Repositories;
using KoksyApp.Dtos.Models;
using MongoDB.Bson;

namespace KoksyApp.API.Services;

public interface IWorkoutDayService
{
    bool AddWorkoutDay(WorkoutDayForCreation forCreation);
    bool AssignUser(string dayId, string userId);
    WorkoutDay[] GetWorkoutDays();
    Task<WorkoutDay> GetWorkoutDay(string id);
}

public class WorkoutDayService:IWorkoutDayService
{
    private readonly IWorkoutDayRepository workoutDayRepository;
    private readonly IUserDayRepository userDayRepository;

    public WorkoutDayService(IWorkoutDayRepository workoutDayRepository, IUserDayRepository userDayRepository)
    {
        this.workoutDayRepository = workoutDayRepository;
        this.userDayRepository = userDayRepository;
    }

    public bool AddWorkoutDay(WorkoutDayForCreation forCreation)
    {
        var workoutDay = new WorkoutDay(forCreation.Name);
        workoutDayRepository.Add(workoutDay);
        return true;
    }

    public bool AssignUser(string dayId, string userId)
    {
        userDayRepository.Add(dayId, userId);
        return true;
    }

    public WorkoutDay[] GetWorkoutDays()
    {
        return workoutDayRepository.GetWorkoutDays();
    }

    //TODO ADd maybe userId
    public Task<WorkoutDay> GetWorkoutDay(string id)
    {
        return workoutDayRepository.GetWorkoutDay(id);
    }
}