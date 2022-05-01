using PlayAndEarnAuth.Models;
using PlayAndEarnAuth.Models.Dto;

namespace PlayAndEarnAuth.Services
{
    public interface IUserAuthService
    {
        ValueTask<User> Register(UserRegistration model);

        ValueTask<User?> LoginAsync(UserLogin model);

        ValueTask<User> LogoutAsync();
    }
}
