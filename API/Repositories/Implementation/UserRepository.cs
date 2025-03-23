using API.Data;
using API.DTO.Request_Response;
using API.Entites;
using API.Repositories.Interface;
using System.Security.Cryptography;
using System.Text;

namespace API.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext applicationDb;

        public UserRepository(ApplicationDbContext applicationDb)
        {
            this.applicationDb = applicationDb;
        }

        public bool CheckExistingUser(string username)
        {
            var user=applicationDb.Users.FirstOrDefault(x=>x.Usermame == username);
            if (user is null)
            {
                return true;
            }
            return false;
            
        }

        public bool CheckUserNamePassword(string username, string password)
        {
            var user = applicationDb.Users.FirstOrDefault(x=>x.Usermame == username);
            if (user is null)
            {
                return false;
            }
            else {
                if (VerifyPassword(password,user.PasswordHash)) { 
                    return true;
                }
            }
            return false;
        }
        public static string EncodePassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            string hashInput = EncodePassword(password);
            return hashInput == hashedPassword;
        }
        public string Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(User req)
        {
            
            await applicationDb.Users.AddAsync(req);
            await applicationDb.SaveChangesAsync();
            return req;
        }

        public Guid ReturnIdUser(string username)
        {
            var user = applicationDb.Users.FirstOrDefault(x => x.Usermame == username);
            if (user is null)
            {
                return Guid.Empty;
            }
            return user.Id;
        }
    }
}
