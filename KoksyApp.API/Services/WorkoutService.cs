using KoksyApp.API.Models;
using KoksyApp.API.Repositories;
using KoksyApp.Dtos.Models;
using MongoDB.Bson;

namespace KoksyApp.API.Services;

public interface IWorkoutService
{
   Workout[] GetWorkoutsForDay(string dayId);

   Task<bool> AddWorkout(WorkoutForCreation forCreation);
}

public class WorkoutService :IWorkoutService
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IWorkoutDayService _workoutDayService;

    public WorkoutService(IWorkoutRepository workoutRepository, IWorkoutDayService workoutDayService)
    {
        _workoutRepository = workoutRepository;
        _workoutDayService = workoutDayService;
    }

    public  Workout[] GetWorkoutsForDay(string dayId)
    {
        return _workoutRepository.GetWorkoutsForDay(dayId);
    }

    public async Task<bool> AddWorkout(WorkoutForCreation forCreation)
    {
        var workout = new Workout(forCreation.WorkoutDayId, forCreation.MinReps, forCreation.MaxReps, forCreation.SessionsCount,
            forCreation.Name, forCreation.Url.AbsoluteUri);
        var workoutDay = await  _workoutDayService.GetWorkoutDay(forCreation.WorkoutDayId);
        if (workoutDay.Id != null)
        {
            workout.WorkoutDayId = workoutDay.Id;
        }
        await _workoutRepository.AddWorkout(workout);
        return true;
    }
}