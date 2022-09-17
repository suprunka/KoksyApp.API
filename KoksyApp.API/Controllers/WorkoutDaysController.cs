using KoksyApp.API.Models;
using KoksyApp.API.Services;
using KoksyApp.Dtos.Models;
using Microsoft.AspNetCore.Mvc;

namespace KoksyApp.API.Controllers;

public class WorkoutDaysController : BaseController
{
    private readonly ILogger<WorkoutDaysController> logger;
    private readonly IWorkoutDayService dayService;

    public WorkoutDaysController(ILogger<WorkoutDaysController> logger,IWorkoutDayService dayService )
    {
        this.logger = logger;
        this.dayService = dayService;
    }

    [HttpGet(Name = "GetWorkoutDays")]
    public WorkoutDay[] Get()
    {
        var days = dayService.GetWorkoutDays();
        return days;
    }
    [HttpPost(Name = "PostWorkoutDays")]
    public bool Post(WorkoutDayForCreation forCreation)
    {
        return dayService.AddWorkoutDay(forCreation);
    }
}