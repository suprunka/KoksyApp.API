using KoksyApp.API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace KoksyApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController :ControllerBase
{
    private readonly IUserAuthService userAuthService;
    private string userId;
    protected string UserId => userId != string.Empty ? userId : GetUser();

    public BaseController(IUserAuthService userAuthService)
    {
        this.userAuthService = userAuthService;
    }
    private  string GetUser()
    {
        var token =  HttpContext.GetTokenAsync("access_token").GetAwaiter().GetResult();
        userId = userAuthService.GetTokenUser(token);
        return UserId;
    }
}