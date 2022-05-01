using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlayAndEarnAuth.Models;
using PlayAndEarnAuth.Models.Dto;

namespace PlayAndEarnAuth.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;

        public RoleService(RoleManager<Role> roleManager, UserManager<User> userManager, ApplicationContext applicationContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = applicationContext;
        }

        public async ValueTask<User> AddRoleAsync(User user, string addedRole)
        {
            Role role = await _roleManager.FindByNameAsync(addedRole);

            if (role == null)
            {
                throw new ArgumentException(nameof(role));
            }

            var result = await _userManager.AddToRoleAsync(user, addedRole);

            if (result.Succeeded)
            {
                return user;
            }

            throw new ArgumentException(nameof(result));
        }

        public async ValueTask<Role?> CreateRoleAsync(RoleDto model)
        {
            var result = await _roleManager.CreateAsync(new Role { Name = model.RoleName });

            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
                Role role = await _roleManager.FindByNameAsync(model.RoleName);

                return role;
            }

            throw new Exception("Something went wrong");
        }

        public async ValueTask<bool> DeleteRoleAsync(string id)
        {
            Role role = await _roleManager.FindByIdAsync(id);

            var result = await _roleManager.DeleteAsync(role) ?? throw new ArgumentNullException(nameof(role));

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async ValueTask<User> DeleteUserRoleAsync(User user, string removedRole)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, removedRole);

            if (result.Succeeded)
            {
                return user;
            }

            return user;
        }

        public async ValueTask<List<Role>> GetListRolesAsync()
        {
            List<Role> roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async ValueTask<IList<string>> GetUserRolesAsync(User user)
        {
            var result = await _userManager.GetRolesAsync(user);

            return result;
        }

        public ValueTask<User> RemoveUserRoleAsync(User user)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Role> SetDefaultRoleAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
