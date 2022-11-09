using KoksyApp.API.Models;
using KoksyApp.API.Services;
using KoksyApp.Dtos.Models;
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
    [Route("/api/workoutdays/{dayId}/workouts")]
    public Workout[] GetWorkoutsForDay(string dayId)
    {
        var workouts = workoutService.GetWorkoutsForDay(dayId);
        return workouts;
    }

    [HttpGet]
    [Route("{exerciseId}")]
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