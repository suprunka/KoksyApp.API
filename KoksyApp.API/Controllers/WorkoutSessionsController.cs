using KoksyApp.API.Models;
using KoksyApp.API.Services;
using KoksyApp.Dtos.Models;
using Microsoft.AspNetCore.Mvc;

namespace KoksyApp.API.Controllers;

public class WorkoutSessionsController : BaseController
{
    private readonly WorkoutSessionService _workoutSessionService;
    
    public WorkoutSessionsController(WorkoutSessionService workoutSessionService)
    {
        _workoutSessionService = workoutSessionService;
    }

    [HttpGet(Name = "GetWorkoutSession")]
    public Task<WorkoutSession> Get(Guid id)
    {
        return _workoutSessionService.GetLastSession(id);
    }
    [HttpPost(Name = "AddWorkoutSession")]
    public Task<bool> Add(WorkoutSessionForCreation session)
    {
        return _workoutSessionService.AddWorkoutSession(session);
    }
}