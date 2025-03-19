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

        [HttpPost("logout", Name = "logoutUser")]
        public async Task<IActionResult> LogoutAsync(int userId)
        {
            try
            {
                var result = await _userService.LogoutAsync(userId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }

        // Create a way to send a code to change a account information (e-mail or phone)s

        [HttpPut("change", Name = "changeLogin")]
        public async Task<IActionResult> ChangeAsync(int receivedCode, string password, string? email)
        {
            try
            {
                var result = await _userService.ChangeLoginAsync(receivedCode, password, email);
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

        [HttpDelete("delete", Name = "deleteUser")]
        public async Task<IActionResult> DeleteAsync(int userId)
        {
            try
            {
                var result = await _userService.DeleteAsync(userId);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception exception)
            {
                return await ErrorResponseController.CreateExceptionResponse(this, exception);
            }
        }
    }
}
