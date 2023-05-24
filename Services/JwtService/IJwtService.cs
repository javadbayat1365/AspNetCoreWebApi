using Entities;
using Entities;

namespace Services
{
    public interface IJwtService
    {
        string Generate(User user);
    }
}