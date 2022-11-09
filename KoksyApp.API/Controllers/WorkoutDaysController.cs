using KoksyApp.API.Models;
using KoksyApp.API.Services;
using KoksyApp.Dtos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.SignalR;

namespace KoksyApp.API.Controllers;

public class WorkoutDaysController : BaseController
{
    private readonly ILogger<WorkoutDaysController> logger;
    private readonly IWorkoutDayService dayService;

    public WorkoutDaysController(ILogger<WorkoutDaysController> logger,
        IWorkoutDayService dayService,
        IUserAuthService authService):base(authService)
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
    [HttpPost("{dayId}")]
    public bool AssignToUser(string dayId)
    {
        return dayService.AssignUser(dayId, UserId);
    } 
    [HttpPost(Name = "PostWorkoutDays")]
    public bool Post(WorkoutDayForCreation forCreation)
    {
        return dayService.AddWorkoutDay(forCreation);
    }
    
    [HttpPatch(Name = "Update")]
    public bool Patch(WorkoutDayForCreation forCreation)
    {
        return dayService.AddWorkoutDay(forCreation);
    }
}