using Entities;
using Entities;

namespace Services
{
    public interface IJwtService
    {
        Task<string> GenerateAsync(User user);
    }
}