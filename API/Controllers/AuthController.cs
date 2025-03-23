using API.DTO.Request_Response;
using API.Entites;
using API.Models;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

        public AuthController(IConfiguration configuration, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _configuration = configuration;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("Chào Admin!");
        }

        [Authorize(Roles = "User")]
        [HttpGet("user-only")]
        public IActionResult UserOnlyEndpoint()
        {
            return Ok("Chào User!");
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserRequest req)
        {
            if (!userRepository.CheckExistingUser(req.Usermame))
            {
                return BadRequest("Username is existing!");
            }
            Roles role = await roleRepository.FindByRoleAsync("User");
            if (role == null) {
                return BadRequest("Role not existing!");
            }
            
            var user = new User
            {
                Usermame = req.Usermame,
                PasswordHash = EncodePassword(req.Password),
                Name=req.Name,
                Email = req.Email,
                PhoneNo = req.PhoneNo,
                Role=role
            };
            user=await userRepository.Register(user);
            return Ok(user);

        }
        
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            try
            {
                if (!userRepository.CheckUserNamePassword(login.UserName, login.Password))
                {
                    return BadRequest("Username or password wrong!");
                }

                
                 
                 string token = GenerateToken(login.UserName);
                 return Ok(GenerateToken(login.UserName));
                
            }
            catch (Exception ex)
            {
                var message = Problem(ex.Message);
                return message;
            }
        }

        private string GenerateToken(string userName)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfiguration:TokenSecret"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            string role = roleRepository.GetRoleNameByUserName(userName);
            var claims = new List<Claim>
            {
                
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)
            };
            
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtConfiguration:Issuer"],
                audience: _configuration["JwtConfiguration:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(180),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static string EncodePassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
        

        //private readonly IConfiguration configuration;

        //public AuthController(IConfiguration configuration)
        //{
        //    this.configuration = configuration;
        //}
        //public static User user = new();
        //[HttpPost("register")]
        //public ActionResult<User> Register(UserDto request)
        //{
        //    var hashedPassword = new PasswordHasher<User>()
        //        .HashPassword(user, request.Password);
        //    user.Usermame = request.Usermame;
        //    user.PasswordHash = hashedPassword;

        //    return Ok(user);
        //}
        //[HttpPost("login")]
        //public ActionResult<string> Login(UserDto request)
        //{
        //    if (user.Usermame != request.Usermame)
        //    {
        //        return BadRequest("User not found");
        //    }
        //    if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) 
        //        == PasswordVerificationResult.Failed)
        //    {
        //        return BadRequest("Wrong password");
        //    }
        //    string token = CreateToken(user);
        //    return Ok(token);

        //} 
        //private string CreateToken(User user)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name,user.Usermame)
        //    };
        //    var key = new SymmetricSecurityKey(
        //        Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!)
        //        );
        //    var creads=new SigningCredentials(key,SecurityAlgorithms.HmacSha512);
        //    var secretKey = configuration.GetValue<string>("AppSettings:Token");
        //    var audi= configuration.GetValue<string>("AppSettings:Audience");
        //    var iss = configuration.GetValue<string>("AppSettings:Issuer");
        //    Console.WriteLine($"Secret Key: {secretKey}");
        //    var tokenDescriptor = new JwtSecurityToken(
        //        issuer:configuration.GetValue<string>("AppSettings:Issuer"),
        //        audience:configuration.GetValue<string>("AppSettings:Audience"),
        //        claims:claims,
        //        expires:DateTime.UtcNow.AddDays(2),
        //        signingCredentials:creads
        //        );
        //    return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        //}

    }
   
}
