using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Property.ApplicationCore.DTOs;
using Property.ApplicationCore.Entities;
using Property.ApplicationCore.Interfaces.Services;
using Property.Infrastructure.Data.Context;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Property.WebAPI.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly PropertyDbContext _propertyDbContext;
        private readonly ITokenService _tokenService;

        public AccountController(PropertyDbContext context, ITokenService tokenService)
        {
            _propertyDbContext = context;
            _tokenService = tokenService;
        }

        // POST: AccountController/register
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.Username)) return BadRequest("Username is taken");

            // Provide hashing algorithm to create a password hash
            // "using" -> when we are finished with HMACSHA512 class, is going to be disposed correctly
            using var hmac = new HMACSHA512();

            var user = new User
            {
                Name = registerDTO.Name,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                UserName = registerDTO.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };

            _propertyDbContext.Users.Add(user);
            await _propertyDbContext.SaveChangesAsync();

            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _propertyDbContext.Users.SingleOrDefaultAsync(x => x.UserName == loginDTO.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _propertyDbContext.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
