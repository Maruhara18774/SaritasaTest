using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saritasa.BAL.User;
using Saritasa.Data.Entities;
using ViewModels.User;

namespace Saritasa.BackendApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest input)
        {
            var result = await _userService.Login(input);
            if(result == "Wrong email" || result == "Wrong password") return BadRequest(result);
            return Ok(result);
        }
    }
}
