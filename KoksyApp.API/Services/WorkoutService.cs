using KoksyApp.API.Models;
using KoksyApp.API.Repositories;
using KoksyApp.Dtos.Models;
using MongoDB.Bson;

namespace KoksyApp.API.Services;

public interface IWorkoutService
{
    Workout[] GetWorkoutsForDay(string dayId);
    Task<Workout> GetWorkout(string id);

   Task<bool> AddWorkout(WorkoutForCreation forCreation);
}

public class WorkoutService :IWorkoutService
{
    private readonly IWorkoutRepository workoutRepository;
    private readonly IWorkoutDayService workoutDayService;

    public WorkoutService(IWorkoutRepository workoutRepository, IWorkoutDayService workoutDayService)
    {
        this.workoutRepository = workoutRepository;
        this.workoutDayService = workoutDayService;
    }

    public  Workout[] GetWorkoutsForDay(string dayId)
    {
        return workoutRepository.GetWorkoutsForDay(dayId);
    }

    public Task<Workout> GetWorkout(string id)
    {
        return workoutRepository.GetWorkout(id);
    }

    public async Task<bool> AddWorkout(WorkoutForCreation forCreation)
    {
        var workout = new Workout(forCreation.WorkoutDayId, forCreation.MinReps, forCreation.MaxReps, forCreation.SessionsCount,
            forCreation.Name, forCreation.Url.AbsoluteUri, forCreation.BreakSeconds);
        var workoutDay = await  workoutDayService.GetWorkoutDay(forCreation.WorkoutDayId);
        if (workoutDay.Id != null)
        {
            workout.WorkoutDayId = workoutDay.Id;
        }
        await workoutRepository.AddWorkout(workout);
        return true;
    }
}