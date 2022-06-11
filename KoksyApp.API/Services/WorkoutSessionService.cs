using KoksyApp.API.Models;
using KoksyApp.API.Repositories;

namespace KoksyApp.API.Services;

interface IWorkoutSessionService
{
    public Task<bool> AddWorkoutSession(WorkoutSessionForCreation session);
    public Task<WorkoutSession> GetLastSession(int id);
}
public class WorkoutSessionService :IWorkoutSessionService
{
    private readonly IWorkoutSessionRepository _sessionRepository;

    public WorkoutSessionService(IWorkoutSessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<bool> AddWorkoutSession(WorkoutSessionForCreation forCreation)
    {
        var session = new WorkoutSession(forCreation);
        await  _sessionRepository.AddWorkoutSession(session);
        //TODO: return in adding
        return true;
    }

    public Task<WorkoutSession> GetLastSession(int id)
    {
        return _sessionRepository.GetLastSession(id);
    }
}
//haslo Bhv5S6iRoTtcxzKs