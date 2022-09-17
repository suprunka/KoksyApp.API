using KoksyApp.API.Models;
using KoksyApp.API.Services;
using KoksyApp.Dtos.Models;
using Microsoft.AspNetCore.Mvc;

namespace KoksyApp.API.Controllers;

public class WorkoutsController : BaseController
{
    private readonly IWorkoutService workoutService;

    public WorkoutsController(IWorkoutService workoutService)
    {
        this.workoutService = workoutService;
    }

    [HttpGet(Name = "GetWorkoutsForDay")]
    public Workout[] Get(string dayId, string exerciseId)
    {
        var workouts = workoutService.GetWorkoutsForDay(dayId);
        return workouts;
    }

    [HttpGet(Name = "GetWorkout")]
    public async Task<Workout> Get(string exerciseId)
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