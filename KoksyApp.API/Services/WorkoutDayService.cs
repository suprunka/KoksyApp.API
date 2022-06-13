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
    private readonly IWorkoutDayRepository _workoutDayRepository;

    public WorkoutDayService(IWorkoutDayRepository workoutDayRepository)
    {
        _workoutDayRepository = workoutDayRepository;
    }

    public bool AddWorkoutDay(WorkoutDayForCreation forCreation)
    {
        var workoutDay = new WorkoutDay(forCreation.Name);
        _workoutDayRepository.Add(workoutDay);
        return true;
    }

    public WorkoutDay[] GetWorkoutDays()
    {
        return _workoutDayRepository.GetWorkoutDays();
    }

    public Task<WorkoutDay> GetWorkoutDay(string id)
    {
        return _workoutDayRepository.GetWorkoutDay(id);
    }
}