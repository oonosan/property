using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Property.ApplicationCore.DTOs;
using Property.ApplicationCore.Entities;
using Property.Infrastructure.Data.Context;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Property.WebAPI.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly PropertyDbContext _propertyDbContext;

        public AccountController(PropertyDbContext context)
        {
            _propertyDbContext = context;
        }

        // POST: AccountController/register
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.Username)) return BadRequest("Username is taken");

            // Provide hashing algorithm to create a password hash
            // "using" -> when we are finished with HMACSHA512 class, is going to be disposed correctly
            using var hmac = new HMACSHA512();

            var user = new User
            {
                Name = "UserName",
                LastName = "UserLastName",
                Email = registerDTO.Email,
                Username = registerDTO.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };

            _propertyDbContext.Users.Add(user);
            await _propertyDbContext.SaveChangesAsync();
            
            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginDTO loginDTO)
        {
            var user = await _propertyDbContext.Users.FirstOrDefaultAsync(x => x.Username == loginDTO.Username);

            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return user;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _propertyDbContext.Users.AnyAsync(x => x.Username == username.ToLower());
        }
    }
}
