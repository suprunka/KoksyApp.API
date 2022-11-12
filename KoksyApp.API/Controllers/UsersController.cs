using KoksyApp.API.Models;
using KoksyApp.API.Models.DTO;
using KoksyApp.API.Services;
using KoksyApp.Dtos.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace KoksyApp.API.Controllers;

public class UsersController: BaseController
{
    private readonly ILogger<UsersController> logger;
    private readonly IUserAuthService userAuthService;

    public UsersController(
        ILogger<UsersController> logger,
        IUserAuthService userAuthService): base(userAuthService)
    {
        this.logger = logger;
        this.userAuthService = userAuthService;
    }

    [HttpPost]
    [Route("login")]
    public ActionResult<string> Login(UserForCreation forCreation)
    {
        var result =  userAuthService.Authenticate(forCreation.Email, forCreation.Password);
        return Ok(result);
    }
    [HttpPost]
    [Route("register")]
    public async Task<bool> Register(UserForCreation forCreation)
    {
        var result =  await userAuthService.Register(forCreation.Email, forCreation.Password);
        return result;
        
    }
}