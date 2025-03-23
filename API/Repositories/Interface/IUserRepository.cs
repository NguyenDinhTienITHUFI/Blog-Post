using API.DTO.Request_Response;
using API.Entites;

namespace API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> Register(User req);
        Boolean CheckExistingUser(string username);
        string Login(string username, string password);
        Boolean CheckUserNamePassword(string username, string password);
        Guid ReturnIdUser(string username);
    }
}
