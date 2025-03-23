using API.Entites;

namespace API.Repositories.Interface
{
    public interface IRoleRepository
    {
        Task<Roles> CreateRole(Roles role);
        Task<Roles?> FindByRoleAsync(string roleName);
        string GetRoleNameByUserName(string userName);
    }
}
