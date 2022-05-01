using PlayAndEarnAuth.Models;
using PlayAndEarnAuth.Models.Dto;

namespace PlayAndEarnAuth.Services
{
    public interface IRoleService
    {
        ValueTask<IList<string>> GetUserRolesAsync(User user);

        ValueTask<List<Role>> GetListRolesAsync();

        ValueTask<User> AddRoleAsync(User user, string addedRole);

        ValueTask<User> DeleteUserRoleAsync(User user, string removedRole);

        ValueTask<Role> SetDefaultRoleAsync(User user);

        ValueTask<Role?> CreateRoleAsync(RoleDto model);

        ValueTask<bool> DeleteRoleAsync(string id);

        ValueTask<User> RemoveUserRoleAsync(User user);
    }
}
