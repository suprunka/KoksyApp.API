using System.Security.Claims;
using KoksyApp.API.Models;
using KoksyApp.API.Services;
using KoksyApp.Dtos.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace KoksyApp.API.Controllers;

public class WorkoutSessionsController : BaseController
{
    private readonly IWorkoutSessionService workoutSessionService;
    private readonly IUserAuthService userAuthService;

    public WorkoutSessionsController(IWorkoutSessionService workoutSessionService,
        IUserAuthService userAuthService)
    {
        this.workoutSessionService = workoutSessionService;
        this.userAuthService = userAuthService;
    }

    [HttpGet]
    [Route("api/users/{userId}/Workout/{id}/Sessions")]

    public Task<WorkoutSession> Get(string id, string userId)
    {
        return workoutSessionService.GetLastSession(id,userId);
    }
    [HttpPost(Name = "AddWorkoutSession")]

    public async Task<bool> Add(WorkoutSessionForCreation session)
    {
        var token =  await HttpContext.GetTokenAsync("access_token");
        var userId = userAuthService.GetTokenUser(token);
        return await workoutSessionService.AddWorkoutSession(session, userId);
    }
}