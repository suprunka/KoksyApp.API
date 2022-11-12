using KoksyApp.API.Models;
using KoksyApp.API.Models.DTO;
using KoksyApp.API.Services;
using KoksyApp.Dtos.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace KoksyApp.API.Controllers;

public class WorkoutsController : BaseController
{
    private readonly IWorkoutService workoutService;

    public WorkoutsController(
        IWorkoutService workoutService,
        IUserAuthService authService): base(authService)
    {
        this.workoutService = workoutService;
    }

    [HttpGet]
    [Microsoft.AspNetCore.Mvc.Route("/api/workoutdays/{dayId}/workouts")]
    public Workout[] GetWorkoutsForDay(string dayId, [FromQuery]string? userId)
    {
        return workoutService.GetWorkoutsForDay(dayId,
            string.IsNullOrEmpty(userId) ? UserId
                : Maybe<string>.Of(userId)
                );
    }

    [HttpGet]
    [Microsoft.AspNetCore.Mvc.Route("{exerciseId}")]
    public async Task<Workout> GetWorkout(string exerciseId)
    {
        var workout = await workoutService.GetWorkout(exerciseId);
        return workout;
    }

    [HttpPost(Name = "Add")]
    public async Task<bool> Add(WorkoutForCreation forCreation)
    {
        var added = await workoutService.AddWorkout(forCreation);
        return added;
    }
}