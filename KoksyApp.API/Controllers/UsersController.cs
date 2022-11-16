using KoksyApp.API.Models.DTO;
using KoksyApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace KoksyApp.API.Controllers;

public class UsersController:ControllerBase
{
    private readonly ILogger<UsersController> logger;
    private readonly IUserAuthService userAuthService;

    public UsersController(
        ILogger<UsersController> logger,
        IUserAuthService userAuthService)
    {
        this.logger = logger;
        this.userAuthService = userAuthService;
    }

    [HttpPost]
    [Route("login")]
    public ActionResult<string> Login(UserForLogin forCreation)
    {
        var result =  userAuthService.Authenticate(forCreation.Email, forCreation.Password);
        return Ok(result);
    }
    [HttpPost]
    [Route("register")]
    public async Task<bool> Register(UserForCreation forCreation)
    {
        var result =  await userAuthService.Register(forCreation.Email, forCreation.Password, "forCreation.Name");
        return result;
        
    }
}