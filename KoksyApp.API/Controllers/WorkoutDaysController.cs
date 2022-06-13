using KoksyApp.API.Models;
using KoksyApp.API.Services;
using KoksyApp.Dtos.Models;
using Microsoft.AspNetCore.Mvc;

namespace KoksyApp.API.Controllers;

public class WorkoutDaysController : BaseController
{
    private readonly ILogger<WorkoutDaysController> _logger;
    private readonly IWorkoutDayService _dayService;

    public WorkoutDaysController(ILogger<WorkoutDaysController> logger,IWorkoutDayService dayService )
    {
        _logger = logger;
        _dayService = dayService;
    }

    [HttpGet(Name = "GetWorkoutDays")]
    public WorkoutDay[] Get()
    {
        var days = _dayService.GetWorkoutDays();
        return days;
    }
    [HttpPost(Name = "PostWorkoutDays")]
    public bool Post(WorkoutDayForCreation forCreation)
    {
        return _dayService.AddWorkoutDay(forCreation);
    }
}