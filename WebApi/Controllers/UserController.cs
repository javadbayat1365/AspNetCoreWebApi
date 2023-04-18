using Data.Contracts;
using Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.ControlServices;
using Services.ControlServices.GenericControlServices;
using WebFramework.Api;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IGenericService<User> genericService;

    public UserController(IGenericService<User> genericService)
    {
        this.genericService = genericService;
    }
    [HttpGet("{Id:int}")]
    public async Task<User> GetById(long Id,CancellationToken cancellationToken) 
        =>
        await genericService.GetById(Id, cancellationToken);
    
    [HttpGet("[action]")]
    public async Task<ApiResult<List<User>>> GetAll(long Id, CancellationToken cancellationToken)
        => await genericService.GetAll(cancellationToken);
    [HttpPost]
    public async Task<ApiResult<User>> Create(User user, CancellationToken cancellationToken)
    {
       var result = await genericService.Create(user, cancellationToken);
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
    
}
