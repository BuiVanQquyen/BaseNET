using ExCore.RFS.Data;
using ExCore.RFS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExCore.RFS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<UserResource>> GetUserById(long id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task Create(UserCreate user)
        {
           await _userService.Create(user);
        }
    }
}
