using BattleshipAPI.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BattleshipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly DbContext _dbContext;

        public UserController(IConfiguration configuration, DbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto req)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(req.Password);

            user.UserName = req.UserName;
            user.PasswordHash = passwordHash;

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<User> Login(UserDto req)
        {
            if(user.UserName != req.UserName) 
            {
                return BadRequest("User not found");
            }
            if(!BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash)) 
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        [HttpPut("update-password")]
        public ActionResult UpdatePassword(UpdatePasswordDto req)
        {
            if (user.UserName != req.UserName)
            {
                return BadRequest("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(req.OldPassword, user.PasswordHash))
            {
                return BadRequest("Wrong old password.");
            }

            var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(req.NewPassword);
            user.PasswordHash = newPasswordHash;

            // You can save the updated user data to your database here

            return Ok("Password updated successfully");
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
