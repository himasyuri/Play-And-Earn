using PlayAndEarnAuth.Models;

namespace PlayAndEarnAuth.Services
{
    public interface IJwtGeneratorService
    {
        ValueTask<string> CreateAccessToken(User user);
    }
}
