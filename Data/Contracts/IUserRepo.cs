using Entities.User;

namespace Data.Contracts
{
    public interface IUserRepo:IGenericRepo<User>
    {
        Task<User?> GetByUsernameAndPassword(string username, int age, CancellationToken cancellationToken);
    }
}