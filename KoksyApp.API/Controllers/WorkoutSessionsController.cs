using KoksyApp.API.Models;
using KoksyApp.API.Services;
using KoksyApp.Dtos.Models;
using Microsoft.AspNetCore.Mvc;

namespace KoksyApp.API.Controllers;

public class WorkoutSessionsController : BaseController
{
    private readonly WorkoutSessionService workoutSessionService;
    
    public WorkoutSessionsController(WorkoutSessionService workoutSessionService)
    {
        this.workoutSessionService = workoutSessionService;
    }

    [HttpGet]
    [Route("api/users/{userId}/Workout/{id}/Sessions")]

    public Task<WorkoutSession> Get(string id, string userId)
    {
        return workoutSessionService.GetLastSession(id,userId);
    }
    [HttpPost(Name = "AddWorkoutSession")]

    public Task<bool> Add(WorkoutSessionForCreation session)
    {
        return workoutSessionService.AddWorkoutSession(session);
    }
}