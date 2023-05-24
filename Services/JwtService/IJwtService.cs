using Entities;
using Entities.User;

namespace Services
{
    public interface IJwtService
    {
        string Generate(User user);
    }
}