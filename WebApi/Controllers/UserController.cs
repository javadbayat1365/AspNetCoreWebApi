using Common.Utilities;
using Data;
using Data.Contracts;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.ControlServices;
using System.Security.Claims;
using WebApi.Models;
using WebFramework.Api;
using WebFramework.FilterActions;

namespace WebApi.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
[ApiResultFilter]
public class UserController : Controller
{
    private readonly IUserServices _userService;
    private readonly IJwtService _jwtService;
    private readonly UserManager<User> _userManager;
    private readonly IUserRepo _userRepo;

    public UserController(IUserServices userService,IJwtService jwtService
        ,UserManager<User> userManager
        ,IUserRepo userRepo
        )
    {
        _userService = userService;
        _jwtService = jwtService;
        _userManager = userManager;
        this._userRepo = userRepo;
    }

    public void log()
    {
        _userRepo.GetByUsernameAndPassword("","",HttpContext.RequestAborted);
    }

    //[HttpGet("{Id:long}")] //<==> Rout Constraint
    [HttpGet("[action]")]
    public async Task<User> GetById(long Id, CancellationToken cancellationToken) //Id with Routing => api/User/id
    {
        var userName = HttpContext.User.Identity.FindFirstValue(ClaimTypes.Name);
        var useridint = HttpContext.User.Identity?.GetUserId<int>();
        var userrole = HttpContext.User.Identity.FindFirstValue(ClaimTypes.Role);

        _userRepo.ExistBeforByUserName(userName);
       return await _userService.GetById(Id, cancellationToken);

    }
    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<string> GetToken(string username,string password,CancellationToken cancellationToken)
    {
        var user =await _userService.GetUserByUsernameAndPasswordAsync(username, password, cancellationToken);

        #region UserManager Example
            //var sel1 = await _userManager.GetUserAsync(HttpContext.User);
            //var tokenResetPassword = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var result = await _userManager.ResetPasswordAsync(user,tokenResetPassword,"");
        #endregion

        var token =await _jwtService.GenerateAsync(user);
        return token;
    }
    [HttpGet("[action]")]
    [Authorize(Roles ="Admin")]
    public async Task<ApiResult<List<User>>> GetAll(long Id, CancellationToken cancellationToken)//Id Query String => api/User?id=1
        => await _userService.GetAll(cancellationToken);
    [HttpPost("[action]")]
    public async Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
    {
        var user = new User();
        MapObjects.MapObject(userDto, user);
        
       var result = await _userService.AddUserAsync(user, cancellationToken);
        return result;
    }
    [HttpDelete("[action]")]
    public async Task<ApiResult<OkResult>> DeleteById(long Id,CancellationToken cancellationToken)
    => Ok(await _userService.Delete(Id, cancellationToken));
    [HttpDelete("[action]")]
    public async Task<ApiResult<object>> DeleteByUser(User user, CancellationToken cancellationToken)
    =>Ok(await _userService.Delete(user, cancellationToken));
    [HttpPut]
    public async Task<ApiResult<User>> Update(User user, CancellationToken cancellationToken)
    {
        await _userService.Update(user, cancellationToken);
        return Ok();
    }
    [HttpGet]
    public async Task<JsonResult> get()
    {
        return new JsonResult("");
    }
}

public class test
{
    private readonly AppDBContext _appDBContext;
    private readonly UserManager<User> _userManager;

    public test(AppDBContext appDBContext,UserManager<User> userManager)
    {
        this._appDBContext = appDBContext;
        this._userManager = userManager;
    }
}
