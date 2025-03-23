using API.Data;
using API.Entites;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext applicationDb;

        public RoleRepository(ApplicationDbContext applicationDb) {
            this.applicationDb = applicationDb;
        }
        public async Task<Roles> CreateRole(Roles role)
        {
            await applicationDb.Roles.AddAsync(role);
            await applicationDb.SaveChangesAsync();
            return role;
        }

        public async Task<Roles?> FindByRoleAsync(string roleName)
        {
            var existingRole = await applicationDb.Roles
                .FirstOrDefaultAsync(x => x.RoleName == roleName);

            return existingRole;
        }

        public string GetRoleNameByUserName(string userName)
        {
            User user = applicationDb.Users.FirstOrDefault(x => x.Usermame == userName);
            if (user == null)
            {
                return null; 
            }

            Roles role = applicationDb.Roles.FirstOrDefault(x => x.RoleId == user.RoleId);
            return role.RoleName;
        }
    }
}
