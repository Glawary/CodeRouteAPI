using CodeRoute.DTO;
using CodeRoute.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult RegisterUser([FromBody] UserLogInfo user)
        {
            bool result = _userService.RegisterUser(user);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
