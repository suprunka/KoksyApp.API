using KoksyApp.API.Models;
using KoksyApp.API.Repositories;
using KoksyApp.Dtos.Models;

namespace KoksyApp.API.Services;

interface IWorkoutSessionService
{
    public Task<bool> AddWorkoutSession(WorkoutSessionForCreation session);
    public Task<WorkoutSession> GetLastSession(string id, string userId);
}
public class WorkoutSessionService :IWorkoutSessionService
{
    private readonly IWorkoutSessionRepository sessionRepository;

    public WorkoutSessionService(IWorkoutSessionRepository sessionRepository)
    {
        this.sessionRepository = sessionRepository;
    }

    public async Task<bool> AddWorkoutSession(WorkoutSessionForCreation forCreation)
    {
        var session = new WorkoutSession(forCreation);
        await  sessionRepository.AddWorkoutSession(session);
        //TODO: return in adding
        return true;
    }

    public Task<WorkoutSession> GetLastSession(string id, string userId)
    {
        return sessionRepository.GetLastSession(id, userId);
    }
}
//haslo Bhv5S6iRoTtcxzKs