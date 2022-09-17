using KoksyApp.API.Models;
using KoksyApp.API.Repositories;
using KoksyApp.Dtos.Models;
using MongoDB.Bson;

namespace KoksyApp.API.Services;

public interface IWorkoutDayService
{
    bool AddWorkoutDay(WorkoutDayForCreation forCreation);
    WorkoutDay[] GetWorkoutDays();
    Task<WorkoutDay> GetWorkoutDay(string id);
}

public class WorkoutDayService:IWorkoutDayService
{
    private readonly IWorkoutDayRepository workoutDayRepository;

    public WorkoutDayService(IWorkoutDayRepository workoutDayRepository)
    {
        this.workoutDayRepository = workoutDayRepository;
    }

    public bool AddWorkoutDay(WorkoutDayForCreation forCreation)
    {
        var workoutDay = new WorkoutDay(forCreation.Name);
        workoutDayRepository.Add(workoutDay);
        return true;
    }

    public WorkoutDay[] GetWorkoutDays()
    {
        return workoutDayRepository.GetWorkoutDays();
    }

    public Task<WorkoutDay> GetWorkoutDay(string id)
    {
        return workoutDayRepository.GetWorkoutDay(id);
    }
}