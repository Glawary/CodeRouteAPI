using CodeRoute.Auth;
using CodeRoute.DTO;
using CodeRoute.Models;
using CodeRoute.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CodeRoute.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private UserService _userService;

        public UserController(UserService userService) 
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("reg")]
        public IActionResult RegUser([FromBody] UserLogInfo user)
        {
            bool result = _userService.RegUser(user);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("auth")]
        public IActionResult AuthUser([FromBody] UserLogInfo user)
        {
            User result = _userService.AuthUser(user);

            if (result.Password == user.Password)
            {
                return Ok(new {token = CreateJWT(result)});
            }

            return BadRequest();
        }


        private string CreateJWT(User user)
        {
            var secretkey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("THIS IS THE SECRET KEY")); // NOTE: SAME KEY AS USED IN Startup.cs FILE
            var credentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

            var claims = new[] // NOTE: could also use List<Claim> here
			{
                new Claim(ClaimTypes.Name, user.UserName), // this will be "User.Identity.Name" value
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.UserId.ToString()) // this could the unique ID assigned to the user by a database
			};

            var token = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER, 
                audience: AuthOptions.AUDIENCE, 
                claims: claims, 
                expires: DateTime.Now.AddMinutes(60), 
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
