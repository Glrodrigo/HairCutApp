using HairCutApp.Domain;
using HairCut.Tools.Service;
using Microsoft.AspNetCore.Mvc;

namespace HairCutApp.Controllers
{
    [Route("v1/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login", Name = "LoginUser")]
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

        [HttpPost("Create", Name = "CreateUser")]
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
