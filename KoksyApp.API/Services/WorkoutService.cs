using KoksyApp.API.Models;
using KoksyApp.API.Repositories;

namespace KoksyApp.API.Services;

interface IWorkoutService
{
    Task<List<Workout>> GetWorkoutsForDay(int dayId);
}

public class WorkoutService :IWorkoutService
{
    private readonly IWorkoutRepository _workoutRepository;

    public WorkoutService(IWorkoutRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }

    public  Task<List<Workout>> GetWorkoutsForDay(int dayId)
    {
        return _workoutRepository.GetWorkoutsForDay(dayId);
    }
}