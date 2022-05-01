using Microsoft.AspNetCore.Identity;
using PlayAndEarnAuth.Models;
using PlayAndEarnAuth.Models.Dto;

namespace PlayAndEarnAuth.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;
        private readonly SignInManager<User> _signInManager;

        public UserAuthService(UserManager<User> userManager, ApplicationContext context, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public async ValueTask<User?> LoginAsync(UserLogin model)
        {
            User user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                throw new Exception("User login not found");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                return user;
            }

            throw new Exception("Invalid password");
        }

        //TODO: refresh token
        public ValueTask<User> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async ValueTask<User> Register(UserRegistration model)
        {
            User user = new User
            {
                Email = model.Email,
                UserName = model.Email,
            };

            await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "User");
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
