using KoksyApp.API.Models;
using KoksyApp.API.Repositories;

namespace KoksyApp.API.Services;

public interface IWorkoutDayService
{
    Task<List<WorkoutDay>> GetWorkoutDays();
}

public class WorkoutDayService:IWorkoutDayService
{
    private readonly IWorkoutDayRepository _workoutDayRepository;

    public WorkoutDayService(IWorkoutDayRepository workoutDayRepository)
    {
        _workoutDayRepository = workoutDayRepository;
    }

    public Task<List<WorkoutDay>> GetWorkoutDays()
    {
        return _workoutDayRepository.GetWorkoutDays();
    }
}