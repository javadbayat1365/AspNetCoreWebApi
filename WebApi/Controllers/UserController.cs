using Common.Utilities;
using Data.Contracts;
using Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.ControlServices;
using Services.ControlServices.GenericControlServices;
using WebApi.Models;
using WebFramework.Api;
using WebFramework.FilterActions;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiResultFilter]
public class UserController : Controller
{
    private readonly IUserServices genericService;

    public UserController(IUserServices genericService)
    {
        this.genericService = genericService;
    }
    [HttpGet("{Id:long}")]
    public async Task<User> GetById(long Id,CancellationToken cancellationToken) //Id with Routing => api/User/id
        =>
        await genericService.GetById(Id, cancellationToken);
    
    [HttpGet("[action]")]
    public async Task<ApiResult<List<User>>> GetAll(long Id, CancellationToken cancellationToken)//Id Query String => api/User?id=1
        => await genericService.GetAll(cancellationToken);
    [HttpPost("[action]")]
    public async Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
    {
        var user = new User();
        MapObjects.MapObject(userDto, user);
        
       var result = await genericService.AddUserAsync(user, cancellationToken);
        return result;
    }
    [HttpDelete("[action]")]
    public async Task<ApiResult<OkResult>> DeleteById(long Id,CancellationToken cancellationToken)
    => Ok(await genericService.Delete(Id, cancellationToken));
    [HttpDelete("[action]")]
    public async Task<ApiResult<object>> DeleteByUser(User user, CancellationToken cancellationToken)
    =>Ok(await genericService.Delete(user, cancellationToken));
    [HttpPut]
    public async Task<ApiResult<User>> Update(User user, CancellationToken cancellationToken)
    {
        await genericService.Update(user, cancellationToken);
        return Ok();
    }
    [HttpGet]
    public async Task<JsonResult> get()
    {
        return new JsonResult("");
    }
}
