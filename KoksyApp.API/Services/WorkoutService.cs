using KoksyApp.API.Models;
using KoksyApp.API.Repositories;
using KoksyApp.Dtos.Models;
using MongoDB.Bson;

namespace KoksyApp.API.Services;

public interface IWorkoutService
{
    Workout[] GetWorkoutsForDay(string dayId, Maybe<string> maybeUserId);
    Task<Workout> GetWorkout(string id);

   Task<bool> AddWorkout(WorkoutForCreation forCreation);
}

public class WorkoutService :IWorkoutService
{
    private readonly IWorkoutRepository workoutRepository;
    private readonly IUserDayRepository userDayRepository;
    private readonly IWorkoutDayService workoutDayService;

    public WorkoutService(IWorkoutRepository workoutRepository,
        IUserDayRepository userDayRepository,
        IWorkoutDayService workoutDayService)
    {
        this.workoutRepository = workoutRepository;
        this.userDayRepository = userDayRepository;
        this.workoutDayService = workoutDayService;
    }

    public  Workout[] GetWorkoutsForDay(string dayId, Maybe<string> maybeUserId)
    {
       maybeUserId.Map<UserDay>(u =>
                userDayRepository.GetUserDay(u,dayId).GetAwaiter().GetResult())
            .Map(d => userDayRepository.Update(d.DayId, d.UserId, DateTime.UtcNow).GetAwaiter().GetResult());
        
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
        workout.WorkoutDayId = workoutDay.Id;
        await workoutRepository.AddWorkout(workout);
        return true;
    }
}