using KoksyApp.API.Models;
using KoksyApp.API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace KoksyApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController :ControllerBase
{
    private readonly IUserAuthService userAuthService;
    protected Maybe<string> UserId =>  GetUser();

    public BaseController(IUserAuthService userAuthService)
    {
        this.userAuthService = userAuthService;
    }
    private Maybe<string> GetUser()
    {
        var token =  Maybe<string>.Of(HttpContext.GetTokenAsync("access_token").GetAwaiter().GetResult());
        return token.Map(t => userAuthService.GetTokenUser(t));
    }
}