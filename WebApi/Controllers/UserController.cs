using Common.Utilities;
using Data.Contracts;
using Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.ControlServices;
using System.Security.Claims;
using WebApi.Models;
using WebFramework.Api;
using WebFramework.FilterActions;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiResultFilter]
public class UserController : Controller
{
    private readonly IUserServices _userService;
    private readonly IJwtService _jwtService;

    public UserController(IUserServices userService,IJwtService jwtService)
    {
        this._userService = userService;
        this._jwtService = jwtService;
    }

    [HttpGet("{Id:long}")]
    public async Task<User> GetById(long Id, CancellationToken cancellationToken) //Id with Routing => api/User/id
    {
        var userid = HttpContext.User.Identity.FindFirstValue(ClaimTypes.Name);
        var useridint = HttpContext.User.Identity?.GetUserId<int>();
        var userrole = HttpContext.User.Identity.FindFirstValue(ClaimTypes.Role);

       return await _userService.GetById(Id, cancellationToken);

    }
    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<string> GetToken(string username,string password,CancellationToken cancellationToken)
    {
        var user =await _userService.GetUserByUsernameAndPasswordAsync(username, password, cancellationToken);
        var token = _jwtService.Generate(user);
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
