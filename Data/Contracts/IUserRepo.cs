using Entities.User;

namespace Data.Contracts
{
    public interface IUserRepo:IGenericRepo<User>
    {
       Task<User?> GetByUsernameAndPassword(string username, string password, CancellationToken cancellationToken);
        Task<bool> ExistBeforByUserName(string UserName);
        Task UpdateSecuirtyStampAsync(User user, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
    }
}