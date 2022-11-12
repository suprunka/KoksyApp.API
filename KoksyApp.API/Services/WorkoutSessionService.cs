using System.Linq;
using System.Threading.Tasks;
using KoksyApp.API.Models;
using KoksyApp.API.Repositories;
using KoksyApp.Dtos.Models;

namespace KoksyApp.API.Services;

public interface IWorkoutSessionService
{
    public Task<bool> AddWorkoutSession(WorkoutSessionForCreation session, string userId);
    public Task<WorkoutSession[]> GetLastSession(string id, string userId);
}
public class WorkoutSessionService :IWorkoutSessionService
{
    private readonly IWorkoutSessionRepository sessionRepository;
    private readonly IWorkoutService workoutService;

    public WorkoutSessionService(IWorkoutSessionRepository sessionRepository, IWorkoutService workoutService)
    {
        this.sessionRepository = sessionRepository;
        this.workoutService = workoutService;
    }

    public async Task<bool> AddWorkoutSession(WorkoutSessionForCreation forCreation, string userId)
    {
        var session = new WorkoutSession(forCreation.Reps, forCreation.SetNumber, forCreation.Weight, forCreation.WorkoutId, userId);
        await  sessionRepository.AddWorkoutSession(session);
        //TODO: return in adding
        return true;
    }

    public async Task<WorkoutSession[]> GetLastSession(string id, string userId)
    {
        var workout = await workoutService.GetWorkout(id);
        var sessions = Enumerable.Range(0, workout.SessionsCount)
            .Select(setNumber => Task.Run((()=>sessionRepository.GetLastSessions(id, userId, setNumber))).GetAwaiter().GetResult())
            .Where(x=> x != null)
            .ToArray();
        return sessions;

    }
}
//haslo Bhv5S6iRoTtcxzKs