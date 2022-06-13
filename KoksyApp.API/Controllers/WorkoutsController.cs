using KoksyApp.API.Models;
using KoksyApp.API.Services;
using KoksyApp.Dtos.Models;
using Microsoft.AspNetCore.Mvc;

namespace KoksyApp.API.Controllers;

public class WorkoutsController :BaseController
{
    private readonly IWorkoutService _workoutService;

    public WorkoutsController(IWorkoutService workoutService)
    {
        _workoutService = workoutService;
    }
    [HttpGet(Name = "GetWorkout")]
    public Workout[] Get(string dayId)
    {
       var workouts = _workoutService.GetWorkoutsForDay(dayId);
       return workouts;
    }

    [HttpPost(Name = "Add")]
    public async Task<bool> Add(WorkoutForCreation forCreation)
    {
        var added = await _workoutService.AddWorkout(forCreation);
        return added;
    }
}