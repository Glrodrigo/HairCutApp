using HairCutApp.Domain;
using HairCut.Tools.Service;
using Microsoft.AspNetCore.Mvc;

namespace HairCutApp.Controllers
{
    [Route("v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login", Name = "loginUser")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDomain login)
        {
            try
            {
                var result = await _userService.LoginAsync(login.Email, login.Password);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        [HttpPost("create", Name = "createUser")]
        public async Task<IActionResult> CreateAsync([FromBody] UserDomain user)
        {
            try
            {
                var result = await _userService.CreateAsync(user.Name, user.Email, user.Password);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }
    }
}
