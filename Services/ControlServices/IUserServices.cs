using Entities.User;
using Services.ControlServices.GenericControlServices;

namespace Services.ControlServices
{
    public interface IUserServices: IGenericService<User>
    {
        Task<User> AddUserAsync(User user,CancellationToken cancellationToken);
    }
}