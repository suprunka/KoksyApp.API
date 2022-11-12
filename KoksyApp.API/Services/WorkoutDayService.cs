using KoksyApp.API.Models;
using KoksyApp.API.Models.DTO;
using KoksyApp.API.Repositories;
using KoksyApp.Dtos.Models;
using MongoDB.Bson;

namespace KoksyApp.API.Services;

public interface IWorkoutDayService
{
    bool AddWorkoutDay(WorkoutDayForCreation forCreation);
    bool AssignUser(string dayId, string userId);
    WorkoutDay[] GetWorkoutDays(Maybe<string> userId);
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

    public WorkoutDay[] GetWorkoutDays(Maybe<string> userId)
    {
       return  userId.Map(id => userDayRepository.GetForUser(id).GetAwaiter().GetResult())
            .Map(userDays=>workoutDayRepository.GetWorkoutDays()
                .Where(d=> userDays.Any(ud=> ud.DayId == d.Id))
                .Select(d=> new WorkoutDay
                {
                    Name = d.Name,
                    Id = d.Id,
                    LastOpened = userDays.FirstOrDefault(ud=> ud.DayId == d.Id).LastOpened
                }))
            .GetValueOrFallback(ArraySegment<WorkoutDay>.Empty)
            .ToArray();
    }

    public Task<WorkoutDay> GetWorkoutDay(string id)
    {
        return workoutDayRepository.GetWorkoutDay(id);
    }
}